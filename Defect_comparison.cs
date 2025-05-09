using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using OpenCvSharp;

public class DefectComparer {
    private readonly string gtFolder;
    private readonly string predFolder;
    private readonly double iouThreshold;

    public DefectComparer(string groundTruthFolder, string predictionFolder, double iouThreshold = 0.5) {
        this.gtFolder = groundTruthFolder;
        this.predFolder = predictionFolder;
        this.iouThreshold = iouThreshold;
    }

    public void Compare() {
        int totalTP = 0, totalFP = 0, totalFN = 0;

        foreach (var gtFile in Directory.GetFiles(gtFolder, "*.xml")) {
            string fileName = Path.GetFileName(gtFile);
            string predFile = Path.Combine(predFolder, fileName);
            if (!File.Exists(predFile)) continue;

            List<Rect> gtBoxes = LoadRectsFromPascalVoc(gtFile);
            List<Rect> predBoxes = LoadRectsFromPascalVoc(predFile);

            EvaluateRects(gtBoxes, predBoxes, iouThreshold, out int TP, out int FP, out int FN);
            totalTP += TP;
            totalFP += FP;
            totalFN += FN;
        }

        double precision = totalTP / (double)(totalTP + totalFP);
        double recall = totalTP / (double)(totalTP + totalFN);
        double f1 = 2 * precision * recall / (precision + recall);

        MessageBox.Show($"Precision: {precision:F2}\nRecall: {recall:F2}\nF1 Score: {f1:F2}",
            "Evaluation Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    private List<Rect> LoadRectsFromPascalVoc(string xmlPath) {
        var rects = new List<Rect>();
        var doc = XDocument.Load(xmlPath);
        var objects = doc.Descendants("object");
        foreach (var obj in objects) {
            var bndbox = obj.Element("bndbox");
            if (bndbox != null) {
                double x1 = double.Parse(bndbox.Element("xmin").Value);
                double y1 = double.Parse(bndbox.Element("ymin").Value);
                double x2 = double.Parse(bndbox.Element("xmax").Value);
                double y2 = double.Parse(bndbox.Element("ymax").Value);
                rects.Add(new Rect((int)x1, (int)y1, (int)(x2 - x1), (int)(y2 - y1)));
            }
        }
        return rects;
    }
    private static void EvaluateRects(List<Rect> groundTruthRects, List<Rect> predictedRects, double iouThreshold,
                                      out int TP, out int FP, out int FN) {
        TP = 0; FP = 0; FN = 0;

        HashSet<int> matchedGT = new HashSet<int>();
        HashSet<int> matchedPred = new HashSet<int>();

        for (int i = 0; i < predictedRects.Count; i++) {
            Rect pred = predictedRects[i];
            for (int j = 0; j < groundTruthRects.Count; j++) {
                //if (matchedGT.Contains(j)) continue;
                Rect gt = groundTruthRects[j];
                double iou = ComputeIoU(pred, gt);
                if (iou >= iouThreshold) {
                    TP++;
                    matchedGT.Add(j);
                    matchedPred.Add(i);
                    break;
                }
            }
        }

        FN = groundTruthRects.Count - matchedGT.Count;
        FP = predictedRects.Count - matchedPred.Count;
    }
    private static double ComputeIoU(Rect a, Rect b) {
        int x1 = Math.Max(a.Left, b.Left);
        int y1 = Math.Max(a.Top, b.Top);
        int x2 = Math.Min(a.Right, b.Right);
        int y2 = Math.Min(a.Bottom, b.Bottom);
        if (x1 == b.Left && x2 == b.Right && y1 == b.Top && y2 == b.Bottom) return 1;// b is subspace of a 
        int interArea = Math.Max(0, x2 - x1) * Math.Max(0, y2 - y1);
        int unionArea = a.Width * a.Height + b.Width * b.Height - interArea;

        return unionArea == 0 ? 0 : (double)interArea / unionArea;
    }
}

using System.ComponentModel;

namespace AudioMonitor;

partial class AccelerometerForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.labelPeakX = new System.Windows.Forms.Label();
            this.formsPlotX = new ScottPlot.FormsPlot();
            this.labelPeakY = new System.Windows.Forms.Label();
            this.formsPlotY = new ScottPlot.FormsPlot();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelPeakX
            // 
            this.labelPeakX.AutoSize = true;
            this.labelPeakX.Location = new System.Drawing.Point(64, 31);
            this.labelPeakX.Name = "labelPeakX";
            this.labelPeakX.Size = new System.Drawing.Size(126, 20);
            this.labelPeakX.TabIndex = 6;
            this.labelPeakX.Text = "Peak Frequency X:";
            // 
            // formsPlotX
            // 
            this.formsPlotX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.formsPlotX.Location = new System.Drawing.Point(11, 55);
            this.formsPlotX.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlotX.Name = "formsPlotX";
            this.formsPlotX.Size = new System.Drawing.Size(850, 1200);
            this.formsPlotX.TabIndex = 7;
            // 
            // labelPeakY
            // 
            this.labelPeakY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPeakY.AutoSize = true;
            this.labelPeakY.Location = new System.Drawing.Point(927, 31);
            this.labelPeakY.Name = "labelPeakY";
            this.labelPeakY.Size = new System.Drawing.Size(125, 20);
            this.labelPeakY.TabIndex = 6;
            this.labelPeakY.Text = "Peak Frequency Y:";
            this.labelPeakY.Click += new System.EventHandler(this.labelPeakY_Click);
            // 
            // formsPlotY
            // 
            this.formsPlotY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlotY.Location = new System.Drawing.Point(877, 58);
            this.formsPlotY.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlotY.Name = "formsPlotY";
            this.formsPlotY.Size = new System.Drawing.Size(850, 1200);
            this.formsPlotY.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AccelerometerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1752, 1313);
            this.Controls.Add(this.formsPlotX);
            this.Controls.Add(this.labelPeakX);
            this.Controls.Add(this.formsPlotY);
            this.Controls.Add(this.labelPeakY);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AccelerometerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accelerometer FFT Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private Label labelPeakX;
    private Label labelPeakY;
    private ScottPlot.FormsPlot formsPlotX;
    private ScottPlot.FormsPlot formsPlotY;
    private System.Windows.Forms.Timer timer1;
}
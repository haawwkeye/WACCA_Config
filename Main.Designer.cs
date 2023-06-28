using System.Xml.Linq;

namespace WACCA_Config
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            SelectPath = new Button();
            SuspendLayout();
            // 
            // Start
            // 
            Start.BackColor = SystemColors.ControlDarkDark;
            Start.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            Start.Location = new Point(182, 325);
            Start.Name = "Start";
            Start.Size = new Size(172, 31);
            Start.TabIndex = 0;
            Start.Text = "Start WACCA Reverse";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // SelectPath
            // 
            SelectPath.BackColor = SystemColors.ControlDarkDark;
            SelectPath.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            SelectPath.Location = new Point(12, 12);
            SelectPath.Name = "SelectPath";
            SelectPath.Size = new Size(172, 31);
            SelectPath.TabIndex = 1;
            SelectPath.Text = "Select WACCA Path";
            SelectPath.UseVisualStyleBackColor = false;
            SelectPath.Click += SelectPath_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(519, 361);
            Controls.Add(SelectPath);
            Controls.Add(Start);
            ForeColor = SystemColors.ControlLightLight;
            MaximizeBox = false;
            MaximumSize = new Size(535, 400);
            MinimizeBox = false;
            MinimumSize = new Size(535, 400);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WACCA Config";
            ResumeLayout(false);
        }

        #endregion

        private Button Start;
        private Button SelectPath;
    }
}
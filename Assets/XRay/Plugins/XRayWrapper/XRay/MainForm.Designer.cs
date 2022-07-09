
namespace XRay
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConsoleBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DumpChunks = new System.Windows.Forms.Button();
            this.BonesTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.FormattingEnabled = true;
            this.ConsoleBox.HorizontalExtent = 2048;
            this.ConsoleBox.HorizontalScrollbar = true;
            this.ConsoleBox.IntegralHeight = false;
            this.ConsoleBox.Location = new System.Drawing.Point(12, 170);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.Size = new System.Drawing.Size(792, 147);
            this.ConsoleBox.TabIndex = 0;
            this.ConsoleBox.TabStop = false;
            // 
            // DumpChunks
            // 
            this.DumpChunks.Location = new System.Drawing.Point(674, 323);
            this.DumpChunks.Name = "DumpChunks";
            this.DumpChunks.Size = new System.Drawing.Size(130, 28);
            this.DumpChunks.TabIndex = 1;
            this.DumpChunks.Text = "Dump chunks";
            this.DumpChunks.UseVisualStyleBackColor = true;
            this.DumpChunks.Click += new System.EventHandler(this.DumpChunks_Click);
            // 
            // BonesTree
            // 
            this.BonesTree.Location = new System.Drawing.Point(12, 25);
            this.BonesTree.Name = "BonesTree";
            this.BonesTree.Size = new System.Drawing.Size(450, 139);
            this.BonesTree.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bones";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 358);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BonesTree);
            this.Controls.Add(this.DumpChunks);
            this.Controls.Add(this.ConsoleBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "X-Ray Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox ConsoleBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button DumpChunks;
        private System.Windows.Forms.TreeView BonesTree;
        private System.Windows.Forms.Label label1;
    }
}


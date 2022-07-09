
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
            this.BonesTree = new System.Windows.Forms.TreeView();
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
            this.ConsoleBox.Size = new System.Drawing.Size(792, 176);
            this.ConsoleBox.TabIndex = 0;
            this.ConsoleBox.TabStop = false;
            // 
            // BonesTree
            // 
            this.BonesTree.Location = new System.Drawing.Point(13, 13);
            this.BonesTree.Name = "BonesTree";
            this.BonesTree.Size = new System.Drawing.Size(496, 135);
            this.BonesTree.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 358);
            this.Controls.Add(this.BonesTree);
            this.Controls.Add(this.ConsoleBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "X-Ray Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox ConsoleBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TreeView BonesTree;
    }
}


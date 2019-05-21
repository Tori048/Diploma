namespace PainCsharp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarConvertToTxt = new System.Windows.Forms.ProgressBar();
            this.progressConvertToTxt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.button8 = new System.Windows.Forms.Button();
            this.Pixel_number = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.OpenManyImageForFirstStep = new System.Windows.Forms.OpenFileDialog();
            this.Matrix = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выбрать первое изображение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(357, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(300, 300);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(669, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(187, 37);
            this.button4.TabIndex = 5;
            this.button4.Text = "Вывести изображение из txt в бокс №";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.FromTxtToTB);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(669, 107);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(186, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Сохранить в txt изображение №";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ImToTxt_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBox1.Location = new System.Drawing.Point(861, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(36, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Разрешение изображения:";
            // 
            // progressBarConvertToTxt
            // 
            this.progressBarConvertToTxt.Location = new System.Drawing.Point(6, 409);
            this.progressBarConvertToTxt.Name = "progressBarConvertToTxt";
            this.progressBarConvertToTxt.Size = new System.Drawing.Size(958, 23);
            this.progressBarConvertToTxt.TabIndex = 12;
            this.progressBarConvertToTxt.Visible = false;
            // 
            // progressConvertToTxt
            // 
            this.progressConvertToTxt.AutoSize = true;
            this.progressConvertToTxt.Location = new System.Drawing.Point(354, 393);
            this.progressConvertToTxt.Name = "progressConvertToTxt";
            this.progressConvertToTxt.Size = new System.Drawing.Size(298, 13);
            this.progressConvertToTxt.TabIndex = 13;
            this.progressConvertToTxt.Text = "Выполняется преобразование изображения в формат txt";
            this.progressConvertToTxt.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Разрешение изображения:";
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBox2.Location = new System.Drawing.Point(862, 67);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(36, 21);
            this.comboBox2.TabIndex = 17;
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.FileName = "openFileDialog4";
            this.openFileDialog4.Multiselect = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(670, 153);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(186, 37);
            this.button8.TabIndex = 20;
            this.button8.Text = "Построить график для пикселя №";
            this.button8.UseVisualStyleBackColor = true;
         //   this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Pixel_number
            // 
            this.Pixel_number.Location = new System.Drawing.Point(861, 162);
            this.Pixel_number.Name = "Pixel_number";
            this.Pixel_number.Size = new System.Drawing.Size(63, 20);
            this.Pixel_number.TabIndex = 21;
            this.Pixel_number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pixel_number_KeyPress);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(669, 209);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(186, 23);
            this.button9.TabIndex = 22;
            this.button9.Text = "Пройтись по изображениям";
            this.toolTip1.SetToolTip(this.button9, "1 - По столбцам напрямую\r\n2 - По столбцам змейкой\r\n3 - По строкам напрямую\r\n4 - П" +
        "о строкам змейкой");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBox3.Location = new System.Drawing.Point(860, 211);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(37, 21);
            this.comboBox3.TabIndex = 23;
            // 
            // OpenManyImageForFirstStep
            // 
            this.OpenManyImageForFirstStep.FileName = "openFileDialog5";
            this.OpenManyImageForFirstStep.Multiselect = true;
            // 
            // Matrix
            // 
            this.Matrix.Location = new System.Drawing.Point(669, 247);
            this.Matrix.Name = "Matrix";
            this.Matrix.Size = new System.Drawing.Size(186, 23);
            this.Matrix.TabIndex = 24;
            this.Matrix.Text = "Построить матрицы ковариаций";
            this.Matrix.UseVisualStyleBackColor = true;
            this.Matrix.Click += new System.EventHandler(this.Matrix_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 443);
            this.Controls.Add(this.Matrix);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.Pixel_number);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressConvertToTxt);
            this.Controls.Add(this.progressBarConvertToTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBarConvertToTxt;
        private System.Windows.Forms.Label progressConvertToTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox Pixel_number;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.OpenFileDialog OpenManyImageForFirstStep;
        private System.Windows.Forms.Button Matrix;
    }
}


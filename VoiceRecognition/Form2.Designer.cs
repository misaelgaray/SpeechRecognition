namespace VoiceRecognition
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_sql = new System.Windows.Forms.TextBox();
            this.txt_natural = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.list_emp = new System.Windows.Forms.ListBox();
            this.txt_todo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 95);
            this.button1.TabIndex = 0;
            this.button1.Text = "Encender";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 261);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 95);
            this.button2.TabIndex = 1;
            this.button2.Text = "Appagado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_sql
            // 
            this.txt_sql.Location = new System.Drawing.Point(12, 99);
            this.txt_sql.Name = "txt_sql";
            this.txt_sql.Size = new System.Drawing.Size(227, 20);
            this.txt_sql.TabIndex = 2;
            // 
            // txt_natural
            // 
            this.txt_natural.Location = new System.Drawing.Point(12, 64);
            this.txt_natural.Name = "txt_natural";
            this.txt_natural.Size = new System.Drawing.Size(227, 20);
            this.txt_natural.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Haz Click Aqui";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // list_emp
            // 
            this.list_emp.FormattingEnabled = true;
            this.list_emp.Location = new System.Drawing.Point(281, 138);
            this.list_emp.Name = "list_emp";
            this.list_emp.Size = new System.Drawing.Size(199, 160);
            this.list_emp.TabIndex = 5;
            this.list_emp.SelectedIndexChanged += new System.EventHandler(this.list_emp_SelectedIndexChanged);
            // 
            // txt_todo
            // 
            this.txt_todo.Location = new System.Drawing.Point(281, 64);
            this.txt_todo.Name = "txt_todo";
            this.txt_todo.Size = new System.Drawing.Size(199, 20);
            this.txt_todo.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 388);
            this.Controls.Add(this.txt_todo);
            this.Controls.Add(this.list_emp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_natural);
            this.Controls.Add(this.txt_sql);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_sql;
        private System.Windows.Forms.TextBox txt_natural;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox list_emp;
        private System.Windows.Forms.TextBox txt_todo;
    }
}
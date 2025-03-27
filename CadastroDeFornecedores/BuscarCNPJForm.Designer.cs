namespace CadastroDeFornecedores
{
    partial class BuscarCNPJForm
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
            this.txtCNPJ = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnBuscarCNPJ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCNPJ
            // 
            this.txtCNPJ.Location = new System.Drawing.Point(91, 71);
            this.txtCNPJ.Name = "txtCNPJ";
            this.txtCNPJ.Size = new System.Drawing.Size(326, 20);
            this.txtCNPJ.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Location = new System.Drawing.Point(91, 115);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(135, 53);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "VOLTAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnBuscarCNPJ
            // 
            this.btnBuscarCNPJ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCNPJ.Location = new System.Drawing.Point(282, 115);
            this.btnBuscarCNPJ.Name = "btnBuscarCNPJ";
            this.btnBuscarCNPJ.Size = new System.Drawing.Size(135, 53);
            this.btnBuscarCNPJ.TabIndex = 1;
            this.btnBuscarCNPJ.Text = "BUSCAR CNPJ";
            this.btnBuscarCNPJ.UseVisualStyleBackColor = true;
            this.btnBuscarCNPJ.Click += new System.EventHandler(this.btnBuscarCNPJ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DIGITE ABAIXO O CNPJ QUE DESEJA ENCONTRAR:";
            // 
            // BuscarCNPJForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 216);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscarCNPJ);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtCNPJ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuscarCNPJForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encontrar ou Buscar Novo CNPJ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCNPJ;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnBuscarCNPJ;
        private System.Windows.Forms.Label label1;
    }
}
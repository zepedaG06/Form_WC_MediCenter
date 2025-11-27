using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormCambiarPasswordPersonal : Form
    {
        private Sistema sistema;
        private PersonalHospitalario personal;

        public FormCambiarPasswordPersonal(Sistema sistemaParam, PersonalHospitalario personalParam)
        {
            sistema = sistemaParam;
            personal = personalParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Cambiar Contraseña";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Cambiar Contraseña";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.Location = new Point(180, 30);
            lblTitulo.Size = new Size(240, 40);
            this.Controls.Add(lblTitulo);

            Label lblActual = new Label();
            lblActual.Text = "Contraseña Actual:";
            lblActual.Font = new Font("Segoe UI", 11);
            lblActual.Location = new Point(80, 100);
            lblActual.Size = new Size(150, 30);
            lblActual.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblActual);

            TextBox txtActual = new TextBox();
            txtActual.Font = new Font("Segoe UI", 11);
            txtActual.Location = new Point(240, 100);
            txtActual.Size = new Size(250, 30);
            txtActual.UseSystemPasswordChar = true;
            this.Controls.Add(txtActual);

            Label lblNueva = new Label();
            lblNueva.Text = "Nueva Contraseña:";
            lblNueva.Font = new Font("Segoe UI", 11);
            lblNueva.Location = new Point(80, 150);
            lblNueva.Size = new Size(150, 30);
            lblNueva.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblNueva);

            TextBox txtNueva = new TextBox();
            txtNueva.Font = new Font("Segoe UI", 11);
            txtNueva.Location = new Point(240, 150);
            txtNueva.Size = new Size(250, 30);
            txtNueva.UseSystemPasswordChar = true;
            this.Controls.Add(txtNueva);

            Label lblConfirmar = new Label();
            lblConfirmar.Text = "Confirmar Contraseña:";
            lblConfirmar.Font = new Font("Segoe UI", 11);
            lblConfirmar.Location = new Point(80, 200);
            lblConfirmar.Size = new Size(150, 30);
            lblConfirmar.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblConfirmar);

            TextBox txtConfirmar = new TextBox();
            txtConfirmar.Font = new Font("Segoe UI", 11);
            txtConfirmar.Location = new Point(240, 200);
            txtConfirmar.Size = new Size(250, 30);
            txtConfirmar.UseSystemPasswordChar = true;
            this.Controls.Add(txtConfirmar);

            Button btnCambiar = new Button();
            btnCambiar.Text = "Cambiar";
            btnCambiar.Font = new Font("Segoe UI", 12);
            btnCambiar.Location = new Point(190, 280);
            btnCambiar.Size = new Size(120, 50);
            btnCambiar.BackColor = Color.White;
            btnCambiar.Click += (s, e) =>
            {
                if (txtActual.Text != personal.Password)
                {
                    MessageBox.Show("Contraseña actual incorrecta", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtNueva.Text.Length < 6)
                {
                    MessageBox.Show("La nueva contraseña debe tener al menos 6 caracteres", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNueva.Text != txtConfirmar.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                personal.Password = txtNueva.Text;
                personal.CambioPassword = true;
                sistema.GuardarTodosDatos();

                MessageBox.Show("Contraseña actualizada exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
            this.Controls.Add(btnCambiar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12);
            btnCancelar.Location = new Point(330, 280);
            btnCancelar.Size = new Size(120, 50);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }
    }
}
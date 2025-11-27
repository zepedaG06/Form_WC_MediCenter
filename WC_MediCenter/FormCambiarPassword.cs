using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormCambiarPassword : Form
    {
        private Sistema sistema;
        private PersonalHospitalario personal;
        private TextBox txtNuevaPassword;
        private TextBox txtConfirmarPassword;

        public FormCambiarPassword(Sistema sistemaParam, PersonalHospitalario personalParam)
        {
            sistema = sistemaParam;
            personal = personalParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Cambio de Contraseña Requerido";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;

            Label lblTitulo = new Label();
            lblTitulo.Text = "Cambio de Contraseña Requerido";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.Location = new Point(120, 30);
            lblTitulo.Size = new Size(360, 40);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            Label lblInfo = new Label();
            lblInfo.Text = "Por seguridad, debe cambiar su contraseña";
            lblInfo.Font = new Font("Segoe UI", 11);
            lblInfo.Location = new Point(130, 80);
            lblInfo.Size = new Size(340, 30);
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInfo);

            Label lblNueva = new Label();
            lblNueva.Text = "Nueva Contraseña:";
            lblNueva.Font = new Font("Segoe UI", 11);
            lblNueva.Location = new Point(80, 140);
            lblNueva.Size = new Size(150, 30);
            lblNueva.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblNueva);

            txtNuevaPassword = new TextBox();
            txtNuevaPassword.Font = new Font("Segoe UI", 11);
            txtNuevaPassword.Location = new Point(240, 140);
            txtNuevaPassword.Size = new Size(250, 30);
            txtNuevaPassword.UseSystemPasswordChar = true;
            this.Controls.Add(txtNuevaPassword);

            Label lblConfirmar = new Label();
            lblConfirmar.Text = "Confirmar Contraseña:";
            lblConfirmar.Font = new Font("Segoe UI", 11);
            lblConfirmar.Location = new Point(80, 190);
            lblConfirmar.Size = new Size(150, 30);
            lblConfirmar.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblConfirmar);

            txtConfirmarPassword = new TextBox();
            txtConfirmarPassword.Font = new Font("Segoe UI", 11);
            txtConfirmarPassword.Location = new Point(240, 190);
            txtConfirmarPassword.Size = new Size(250, 30);
            txtConfirmarPassword.UseSystemPasswordChar = true;
            this.Controls.Add(txtConfirmarPassword);

            Button btnCambiar = new Button();
            btnCambiar.Text = "Cambiar Contraseña";
            btnCambiar.Font = new Font("Segoe UI", 12);
            btnCambiar.Location = new Point(200, 270);
            btnCambiar.Size = new Size(200, 50);
            btnCambiar.BackColor = Color.White;
            btnCambiar.Click += BtnCambiar_Click;
            this.Controls.Add(btnCambiar);
        }

        private void BtnCambiar_Click(object sender, EventArgs e)
        {
            string nueva = txtNuevaPassword.Text;
            string confirmar = txtConfirmarPassword.Text;

            if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmar))
            {
                MessageBox.Show("Por favor complete todos los campos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            personal.Password = nueva;
            personal.CambioPassword = true;
            sistema.GuardarTodosDatos();

            MessageBox.Show("Contraseña actualizada exitosamente", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
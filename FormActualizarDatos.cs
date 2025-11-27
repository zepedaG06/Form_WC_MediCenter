using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormActualizarDatos : Form
    {
        private Sistema sistema;
        private Paciente paciente;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtContacto;
        private TextBox txtPasswordActual;
        private TextBox txtPasswordNueva;

        public FormActualizarDatos(Sistema sistemaParam, Paciente pacienteParam)
        {
            sistema = sistemaParam;
            paciente = pacienteParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(700, 600);
            this.Text = "Actualizar Datos Personales";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Actualizar Datos Personales";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(180, 20);
            lblTitulo.Size = new Size(340, 40);
            this.Controls.Add(lblTitulo);

            int yPos = 90;

            Label lblTelefono = new Label();
            lblTelefono.Text = "Nuevo Teléfono:";
            lblTelefono.Font = new Font("Segoe UI", 11);
            lblTelefono.Location = new Point(80, yPos);
            lblTelefono.Size = new Size(150, 30);
            lblTelefono.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblTelefono);

            txtTelefono = new TextBox();
            txtTelefono.Font = new Font("Segoe UI", 11);
            txtTelefono.Location = new Point(250, yPos);
            txtTelefono.Size = new Size(300, 30);
            txtTelefono.Text = paciente.Telefono;
            this.Controls.Add(txtTelefono);

            yPos += 50;

            Label lblEmail = new Label();
            lblEmail.Text = "Nuevo Email:";
            lblEmail.Font = new Font("Segoe UI", 11);
            lblEmail.Location = new Point(80, yPos);
            lblEmail.Size = new Size(150, 30);
            lblEmail.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Font = new Font("Segoe UI", 11);
            txtEmail.Location = new Point(250, yPos);
            txtEmail.Size = new Size(300, 30);
            txtEmail.Text = paciente.Email;
            this.Controls.Add(txtEmail);

            yPos += 50;

            Label lblContacto = new Label();
            lblContacto.Text = "Nuevo Contacto Emergencia:";
            lblContacto.Font = new Font("Segoe UI", 11);
            lblContacto.Location = new Point(50, yPos);
            lblContacto.Size = new Size(180, 30);
            lblContacto.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblContacto);

            txtContacto = new TextBox();
            txtContacto.Font = new Font("Segoe UI", 11);
            txtContacto.Location = new Point(250, yPos);
            txtContacto.Size = new Size(300, 30);
            txtContacto.Text = paciente.ContactoEmergencia;
            this.Controls.Add(txtContacto);

            yPos += 80;

            Label lblSeparador = new Label();
            lblSeparador.Text = "Cambiar Contraseña";
            lblSeparador.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSeparador.Location = new Point(240, yPos);
            lblSeparador.Size = new Size(220, 30);
            this.Controls.Add(lblSeparador);

            yPos += 40;

            Label lblPassActual = new Label();
            lblPassActual.Text = "Contraseña Actual:";
            lblPassActual.Font = new Font("Segoe UI", 11);
            lblPassActual.Location = new Point(80, yPos);
            lblPassActual.Size = new Size(150, 30);
            lblPassActual.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblPassActual);

            txtPasswordActual = new TextBox();
            txtPasswordActual.Font = new Font("Segoe UI", 11);
            txtPasswordActual.Location = new Point(250, yPos);
            txtPasswordActual.Size = new Size(300, 30);
            txtPasswordActual.UseSystemPasswordChar = true;
            this.Controls.Add(txtPasswordActual);

            yPos += 50;

            Label lblPassNueva = new Label();
            lblPassNueva.Text = "Nueva Contraseña:";
            lblPassNueva.Font = new Font("Segoe UI", 11);
            lblPassNueva.Location = new Point(80, yPos);
            lblPassNueva.Size = new Size(150, 30);
            lblPassNueva.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(lblPassNueva);

            txtPasswordNueva = new TextBox();
            txtPasswordNueva.Font = new Font("Segoe UI", 11);
            txtPasswordNueva.Location = new Point(250, yPos);
            txtPasswordNueva.Size = new Size(300, 30);
            txtPasswordNueva.UseSystemPasswordChar = true;
            this.Controls.Add(txtPasswordNueva);

            yPos += 70;

            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar Cambios";
            btnGuardar.Font = new Font("Segoe UI", 12);
            btnGuardar.Location = new Point(200, yPos);
            btnGuardar.Size = new Size(160, 45);
            btnGuardar.BackColor = Color.White;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12);
            btnCancelar.Location = new Point(380, yPos);
            btnCancelar.Size = new Size(120, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool cambios = false;

            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && txtTelefono.Text != paciente.Telefono)
            {
                paciente.Telefono = txtTelefono.Text.Trim();
                cambios = true;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text != paciente.Email)
            {
                paciente.Email = txtEmail.Text.Trim();
                cambios = true;
            }

            if (!string.IsNullOrWhiteSpace(txtContacto.Text) && txtContacto.Text != paciente.ContactoEmergencia)
            {
                paciente.ContactoEmergencia = txtContacto.Text.Trim();
                cambios = true;
            }

            if (!string.IsNullOrWhiteSpace(txtPasswordActual.Text) &&
                !string.IsNullOrWhiteSpace(txtPasswordNueva.Text))
            {
                if (txtPasswordActual.Text == paciente.Password)
                {
                    if (txtPasswordNueva.Text.Length >= 6)
                    {
                        paciente.Password = txtPasswordNueva.Text;
                        cambios = true;
                        MessageBox.Show("Contraseña actualizada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("La nueva contraseña debe tener al menos 6 caracteres", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña actual es incorrecta", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (cambios)
            {
                sistema.GuardarTodosDatos();
                MessageBox.Show("Datos actualizados exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se realizaron cambios", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
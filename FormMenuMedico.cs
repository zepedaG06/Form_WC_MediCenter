using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace MEDICENTER
{
    public partial class FormMenuMedico : Form
    {
        private Sistema sistema;
        private PersonalHospitalario medico;

        public FormMenuMedico(Sistema sistemaParam, PersonalHospitalario medicoParam)
        {
            sistema = sistemaParam;
            medico = medicoParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "MediCenter - Menú Médico";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Hospital hospital = sistema.BuscarHospital(medico.IdHospital);

            Panel panelInfo = new Panel();
            panelInfo.Location = new Point(0, 0);
            panelInfo.Size = new Size(900, 100);
            panelInfo.BackColor = Color.FromArgb(100, 150, 200);
            this.Controls.Add(panelInfo);

            Label lblNombreMedico = new Label();
            lblNombreMedico.Text = $"MÉDICO - {medico.Nombre}";
            lblNombreMedico.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNombreMedico.ForeColor = Color.White;
            lblNombreMedico.Location = new Point(30, 20);
            lblNombreMedico.Size = new Size(600, 35);
            panelInfo.Controls.Add(lblNombreMedico);

            Label lblHospital = new Label();
            lblHospital.Text = hospital?.Nombre ?? "Hospital";
            lblHospital.Font = new Font("Segoe UI", 13);
            lblHospital.ForeColor = Color.White;
            lblHospital.Location = new Point(30, 55);
            lblHospital.Size = new Size(600, 30);
            panelInfo.Controls.Add(lblHospital);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Panel Médico";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(350, 120);
            lblTitulo.Size = new Size(200, 40);
            this.Controls.Add(lblTitulo);

            int yPos = 180;
            int xLeft = 100;
            int xRight = 500;

            Button btnAtender = CrearBoton("1. Atender Paciente (Cola)", xLeft, yPos);
            btnAtender.Click += (s, e) => AtenderPaciente();
            this.Controls.Add(btnAtender);

            Button btnValidar = CrearBoton("2. Validar Diagnósticos", xRight, yPos);
            btnValidar.Click += (s, e) => ValidarDiagnosticos();
            this.Controls.Add(btnValidar);

            yPos += 70;

            Button btnRegistros = CrearBoton("3. Ver Registros del Hospital", xLeft, yPos);
            btnRegistros.Click += (s, e) => VerRegistros();
            this.Controls.Add(btnRegistros);

            Button btnMisPacientes = CrearBoton("4. Mis Pacientes Asignados", xRight, yPos);
            btnMisPacientes.Click += (s, e) => VerMisPacientes();
            this.Controls.Add(btnMisPacientes);

            yPos += 70;

            Button btnInfo = CrearBoton("5. Ver mi Información", xLeft, yPos);
            btnInfo.Click += (s, e) => VerInformacion();
            this.Controls.Add(btnInfo);

            Button btnPassword = CrearBoton("6. Cambiar Contraseña", xRight, yPos);
            btnPassword.Click += (s, e) => CambiarPassword();
            this.Controls.Add(btnPassword);

            yPos += 70;

            Button btnCerrarSesion = CrearBoton("0. Cerrar Sesión", xLeft, yPos);
            btnCerrarSesion.BackColor = Color.FromArgb(200, 100, 100);
            btnCerrarSesion.Click += (s, e) => CerrarSesion();
            this.Controls.Add(btnCerrarSesion);
        }

        private Button CrearBoton(string texto, int x, int y)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Font = new Font("Segoe UI", 11);
            btn.Location = new Point(x, y);
            btn.Size = new Size(350, 55);
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void AtenderPaciente()
        {
            FormAtenderPaciente formAtender = new FormAtenderPaciente(sistema, medico);
            formAtender.ShowDialog();
        }

        private void ValidarDiagnosticos()
        {
            FormValidarDiagnosticos formValidar = new FormValidarDiagnosticos(sistema, medico);
            formValidar.ShowDialog();
        }

        private void VerRegistros()
        {
            FormRegistrosMedicosHospital formRegistros =
                new FormRegistrosMedicosHospital(sistema, medico.IdHospital);
            formRegistros.ShowDialog();
        }

        private void VerMisPacientes()
        {
            FormMisPacientes formMisPacientes = new FormMisPacientes(sistema, medico);
            formMisPacientes.ShowDialog();
        }

        private void VerInformacion()
        {
            medico.MostrarInformacion();
        }

        private void CambiarPassword()
        {
            FormCambiarPasswordPersonal formCambio = new FormCambiarPasswordPersonal(sistema, medico);
            formCambio.ShowDialog();
        }

        private void CerrarSesion()
        {
            sistema.GuardarTodosDatos();
            MessageBox.Show("Sesión guardada. Hasta pronto", "Cerrando sesión",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
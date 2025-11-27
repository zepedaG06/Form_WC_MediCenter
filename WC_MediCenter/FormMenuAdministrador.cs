using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormMenuAdministrador : Form
    {
        private Sistema sistema;
        private PersonalHospitalario admin;

        public FormMenuAdministrador(Sistema sistemaParam, PersonalHospitalario adminParam)
        {
            sistema = sistemaParam;
            admin = adminParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "MediCenter - Menú Administrador";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Hospital hospital = sistema.BuscarHospital(admin.IdHospital);

            Panel panelInfo = new Panel();
            panelInfo.Location = new Point(0, 0);
            panelInfo.Size = new Size(900, 100);
            panelInfo.BackColor = Color.FromArgb(200, 100, 100);
            this.Controls.Add(panelInfo);

            Label lblNombreAdmin = new Label();
            if (admin.Id == "ADMIN001")
            {
                lblNombreAdmin.Text = $"ADMINISTRADOR GENERAL - {admin.Nombre}";
            }
            else
            {
                lblNombreAdmin.Text = $"ADMINISTRADOR DEL HOSPITAL - {admin.Nombre}";
            }
            lblNombreAdmin.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNombreAdmin.ForeColor = Color.White;
            lblNombreAdmin.Location = new Point(30, 20);
            lblNombreAdmin.Size = new Size(600, 35);
            panelInfo.Controls.Add(lblNombreAdmin);

            Label lblHospital = new Label();
            lblHospital.Text = hospital?.Nombre ?? "Hospital";
            lblHospital.Font = new Font("Segoe UI", 13);
            lblHospital.ForeColor = Color.White;
            lblHospital.Location = new Point(30, 55);
            lblHospital.Size = new Size(600, 30);
            panelInfo.Controls.Add(lblHospital);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Panel de Administración";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(300, 120);
            lblTitulo.Size = new Size(300, 40);
            this.Controls.Add(lblTitulo);

            int yPos = 180;
            int xLeft = 100;
            int xRight = 500;

            Button btnPersonal = CrearBoton("1. Gestionar Personal del Hospital", xLeft, yPos);
            btnPersonal.Click += (s, e) => GestionarPersonal();
            this.Controls.Add(btnPersonal);

            Button btnEstadisticas = CrearBoton("2. Ver Estadísticas del Hospital", xRight, yPos);
            btnEstadisticas.Click += (s, e) => VerEstadisticas();
            this.Controls.Add(btnEstadisticas);

            yPos += 70;

            Button btnRegistros = CrearBoton("3. Ver Registros Médicos", xLeft, yPos);
            btnRegistros.Click += (s, e) => VerRegistrosMedicos();
            this.Controls.Add(btnRegistros);

            Button btnCola = CrearBoton("4. Ver Cola de Pacientes", xRight, yPos);
            btnCola.Click += (s, e) => VerColaPacientes();
            this.Controls.Add(btnCola);

            yPos += 70;

            Button btnInfo = CrearBoton("5. Ver mi Información", xLeft, yPos);
            btnInfo.Click += (s, e) => VerInformacion();
            this.Controls.Add(btnInfo);

            Button btnPassword = CrearBoton("6. Cambiar Contraseña", xRight, yPos);
            btnPassword.Click += (s, e) => CambiarPassword();
            this.Controls.Add(btnPassword);

            yPos += 70;

            Button btnRegistrar = CrearBoton("7. Registrar Nuevo Personal", xRight, yPos);
            btnRegistrar.Click += (s, e) => RegistrarNuevoPersonal();
            this.Controls.Add(btnRegistrar);

            yPos += 70;

            Button btnCerrarSesion = CrearBoton("0. Cerrar Sesión", xLeft, yPos);
            btnCerrarSesion.BackColor = Color.FromArgb(200, 100, 100);
            btnCerrarSesion.Click += (s, e) => CerrarSesion();
            this.Controls.Add(btnCerrarSesion);
        }
        private void RegistrarNuevoPersonal()
        {
            FormRegistrarPersonalAdmin form = new FormRegistrarPersonalAdmin(sistema, admin);
            form.ShowDialog();
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

        private void GestionarPersonal()
        {
            FormGestionPersonal formGestion = new FormGestionPersonal(sistema, admin);
            formGestion.ShowDialog();
        }

        private void VerEstadisticas()
        {
            Hospital hospital = sistema.BuscarHospital(admin.IdHospital);
            List<RegistroMedico> registros = sistema.RegistrosPorHospital.ContainsKey(admin.IdHospital)
                ? sistema.RegistrosPorHospital[admin.IdHospital]
                : new List<RegistroMedico>();

            Queue<string> cola = sistema.ColasPorHospital.ContainsKey(admin.IdHospital)
                ? sistema.ColasPorHospital[admin.IdHospital]
                : new Queue<string>();

            int confirmados = registros.FindAll(r => r.Confirmado).Count;
            int pendientes = registros.Count - confirmados;

            string info = "═══════════════════════════════════════\n";
            info += "   ESTADÍSTICAS DEL HOSPITAL\n";
            info += "═══════════════════════════════════════\n\n";
            info += $"Hospital: {hospital.Nombre}\n\n";
            info += $"Total de Registros: {registros.Count}\n";
            info += $"Diagnósticos Confirmados: {confirmados}\n";
            info += $"Diagnósticos Pendientes: {pendientes}\n";
            info += $"Pacientes en Cola: {cola.Count}\n";
            info += $"Personal del Hospital: {hospital.PersonalIds.Count}\n";
            info += $"Precisión de Diagnóstico: {hospital.PrecisionDiagnostico}%\n";
            info += $"Tiempo Promedio: {hospital.TiempoPromedioMin} min\n";

            MessageBox.Show(info, "Estadísticas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void VerRegistrosMedicos()
        {
            FormRegistrosMedicosHospital formRegistros =
                new FormRegistrosMedicosHospital(sistema, admin.IdHospital);
            formRegistros.ShowDialog();
        }

        private void VerColaPacientes()
        {
            FormColaPacientes formCola = new FormColaPacientes(sistema, admin.IdHospital);
            formCola.ShowDialog();
        }

        private void VerInformacion()
        {
            admin.MostrarInformacion();
        }

        private void CambiarPassword()
        {
            FormCambiarPasswordPersonal formCambio = new FormCambiarPasswordPersonal(sistema, admin);
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
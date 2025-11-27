using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormMenuPaciente : Form
    {
        private Sistema sistema;
        private Paciente paciente;

        public FormMenuPaciente(Sistema sistemaParam, Paciente pacienteParam)
        {
            sistema = sistemaParam;
            paciente = pacienteParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(900, 650);
            this.Text = "MediCenter - Menú Paciente";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Panel panelInfo = new Panel();
            panelInfo.Location = new Point(0, 0);
            panelInfo.Size = new Size(900, 80);
            panelInfo.BackColor = Color.FromArgb(100, 150, 200);
            this.Controls.Add(panelInfo);

            Label lblNombrePaciente = new Label();
            lblNombrePaciente.Text = $"Paciente: {paciente.Nombre}";
            lblNombrePaciente.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNombrePaciente.ForeColor = Color.White;
            lblNombrePaciente.Location = new Point(30, 25);
            lblNombrePaciente.Size = new Size(500, 35);
            panelInfo.Controls.Add(lblNombrePaciente);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Menú Principal - Paciente";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(300, 100);
            lblTitulo.Size = new Size(350, 40);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            int yPos = 160;
            int xLeft = 100;
            int xRight = 500;

            Button btnSeleccionarHospital = CrearBoton("1. Seleccionar Hospital y Solicitar Consulta", xLeft, yPos);
            btnSeleccionarHospital.Click += (s, e) => SeleccionarHospitalYConsultar();
            this.Controls.Add(btnSeleccionarHospital);

            Button btnHistorial = CrearBoton("2. Ver mi Historial Médico", xRight, yPos);
            btnHistorial.Click += (s, e) => VerHistorial();
            this.Controls.Add(btnHistorial);

            yPos += 70;

            Button btnCompararHospitales = CrearBoton("3. Comparar Hospitales", xLeft, yPos);
            btnCompararHospitales.Click += (s, e) => CompararHospitales();
            this.Controls.Add(btnCompararHospitales);

            Button btnInfoPersonal = CrearBoton("4. Ver mi Información Personal", xRight, yPos);
            btnInfoPersonal.Click += (s, e) => VerInformacionPersonal();
            this.Controls.Add(btnInfoPersonal);

            yPos += 70;

            Button btnActualizarDatos = CrearBoton("5. Actualizar mis Datos", xLeft, yPos);
            btnActualizarDatos.Click += (s, e) => ActualizarDatos();
            this.Controls.Add(btnActualizarDatos);

            Button btnCerrarSesion = CrearBoton("0. Cerrar Sesión", xRight, yPos);
            btnCerrarSesion.BackColor = Color.FromArgb(200, 100, 100);
            btnCerrarSesion.Click += (s, e) => CerrarSesion();
            this.Controls.Add(btnCerrarSesion);

            this.ResumeLayout(false);
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

        private void SeleccionarHospitalYConsultar()
        {
            FormSeleccionarHospital formHospital = new FormSeleccionarHospital(sistema, paciente);
            formHospital.ShowDialog();
        }

        private void VerHistorial()
        {
            FormHistorialPaciente formHistorial = new FormHistorialPaciente(paciente);
            formHistorial.ShowDialog();
        }

        private void CompararHospitales()
        {
            FormCompararHospitales formComparar = new FormCompararHospitales(sistema);
            formComparar.ShowDialog();
        }

        private void VerInformacionPersonal()
        {
            string info = $"═══════════════════════════════════════\n";
            info += $"       INFORMACIÓN DEL PACIENTE\n";
            info += $"═══════════════════════════════════════\n\n";
            info += $"ID: {paciente.Id}\n";
            info += $"Nombre: {paciente.Nombre}\n";
            info += $"Email: {paciente.Email}\n";
            info += $"Edad: {paciente.Edad} años\n";
            info += $"Teléfono: {paciente.Telefono}\n";
            info += $"Género: {paciente.Genero}\n";
            info += $"Tipo de Sangre: {FormatearTipoSangre(paciente.TipoSangre)}\n";
            info += $"Seguro Médico: {FormatearSeguro(paciente.TipoSeguro)}\n";
            if (!string.IsNullOrEmpty(paciente.NumeroSeguro))
                info += $"Número de Seguro: {paciente.NumeroSeguro}\n";
            info += $"Contacto Emergencia: {paciente.ContactoEmergencia}\n";
            info += $"Registros Médicos: {paciente.Historial.Count}\n";

            MessageBox.Show(info, "Información Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarDatos()
        {
            FormActualizarDatos formActualizar = new FormActualizarDatos(sistema, paciente);
            formActualizar.ShowDialog();
        }

        private void CerrarSesion()
        {
            sistema.GuardarTodosDatos();
            MessageBox.Show("Sesión guardada. Hasta pronto", "Cerrando sesión",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private string FormatearTipoSangre(TipoSangre tipo)
        {
            return tipo.ToString().Replace("_Positivo", "+").Replace("_Negativo", "-");
        }

        private string FormatearSeguro(TipoSeguro tipo)
        {
            switch (tipo)
            {
                case TipoSeguro.SinSeguro: return "Sin Seguro";
                case TipoSeguro.SeguroBasico: return "Seguro Básico";
                case TipoSeguro.SeguroCompleto: return "Seguro Completo";
                default: return "No especificado";
            }
        }
    }
}
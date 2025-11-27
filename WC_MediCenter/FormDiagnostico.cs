using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormDiagnostico : Form
    {
        private Sistema sistema;
        private Paciente paciente;
        private Hospital hospital;
        private RegistroMedico registro;
        private DecisionNode nodoActual;
        private Label lblPregunta;
        private Button btnSi;
        private Button btnNo;
        private Panel panelPregunta;
        private Panel panelResultado;

        public FormDiagnostico(Sistema sistemaParam, Paciente pacienteParam, Hospital hospitalParam, RegistroMedico registroParam)
        {
            sistema = sistemaParam;
            paciente = pacienteParam;
            hospital = hospitalParam;
            registro = registroParam;
            nodoActual = sistema.ArbolDiagnostico;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 650);
            this.Text = "Diagnóstico Automático";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Diagnóstico Automático";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 30);
            lblTitulo.Size = new Size(400, 50);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            Label lblInstruccion = new Label();
            lblInstruccion.Text = "Responda las siguientes preguntas:";
            lblInstruccion.Font = new Font("Segoe UI", 13);
            lblInstruccion.Location = new Point(250, 90);
            lblInstruccion.Size = new Size(400, 30);
            lblInstruccion.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblInstruccion);

            panelPregunta = new Panel();
            panelPregunta.Location = new Point(100, 150);
            panelPregunta.Size = new Size(700, 350);
            panelPregunta.BackColor = Color.White;
            panelPregunta.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelPregunta);

            lblPregunta = new Label();
            lblPregunta.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblPregunta.Location = new Point(50, 80);
            lblPregunta.Size = new Size(600, 100);
            lblPregunta.TextAlign = ContentAlignment.MiddleCenter;
            panelPregunta.Controls.Add(lblPregunta);

            btnSi = new Button();
            btnSi.Text = "SÍ";
            btnSi.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSi.Location = new Point(150, 220);
            btnSi.Size = new Size(150, 60);
            btnSi.BackColor = Color.LightGreen;
            btnSi.Click += (s, e) => ProcesarRespuesta("si");
            panelPregunta.Controls.Add(btnSi);

            btnNo = new Button();
            btnNo.Text = "NO";
            btnNo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnNo.Location = new Point(400, 220);
            btnNo.Size = new Size(150, 60);
            btnNo.BackColor = Color.LightCoral;
            btnNo.Click += (s, e) => ProcesarRespuesta("no");
            panelPregunta.Controls.Add(btnNo);

            panelResultado = new Panel();
            panelResultado.Location = new Point(100, 150);
            panelResultado.Size = new Size(700, 350);
            panelResultado.BackColor = Color.White;
            panelResultado.BorderStyle = BorderStyle.FixedSingle;
            panelResultado.Visible = false;
            this.Controls.Add(panelResultado);

            MostrarPregunta();
        }

        private void MostrarPregunta()
        {
            if (nodoActual.EsHoja())
            {
                MostrarResultado();
            }
            else
            {
                lblPregunta.Text = nodoActual.Pregunta;
            }
        }

        private void ProcesarRespuesta(string respuesta)
        {
            foreach (DecisionNode hijo in nodoActual.Hijos)
            {
                if (hijo.RespuestaEsperada == respuesta)
                {
                    nodoActual = hijo;
                    MostrarPregunta();
                    return;
                }
            }

            MessageBox.Show("Error en el árbol de decisión", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarResultado()
        {
            panelPregunta.Visible = false;
            panelResultado.Visible = true;

            string diagnostico = nodoActual.Diagnostico;
            registro.Diagnostico = diagnostico;
            registro.Tratamiento = "Pendiente de revisión médica";

            Label lblResultadoTitulo = new Label();
            lblResultadoTitulo.Text = "Resultado del Diagnóstico";
            lblResultadoTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblResultadoTitulo.Location = new Point(150, 30);
            lblResultadoTitulo.Size = new Size(400, 40);
            lblResultadoTitulo.TextAlign = ContentAlignment.MiddleCenter;
            panelResultado.Controls.Add(lblResultadoTitulo);

            Label lblDiagnostico = new Label();
            lblDiagnostico.Text = diagnostico;
            lblDiagnostico.Font = new Font("Segoe UI", 14);
            lblDiagnostico.Location = new Point(50, 100);
            lblDiagnostico.Size = new Size(600, 120);
            lblDiagnostico.TextAlign = ContentAlignment.MiddleCenter;
            panelResultado.Controls.Add(lblDiagnostico);

            if (diagnostico.Contains("CRITICO"))
            {
                lblDiagnostico.ForeColor = Color.Red;
                Label lblAlerta = new Label();
                lblAlerta.Text = "⚠ ALERTA: Diagnóstico crítico detectado";
                lblAlerta.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblAlerta.ForeColor = Color.Red;
                lblAlerta.Location = new Point(150, 230);
                lblAlerta.Size = new Size(400, 30);
                lblAlerta.TextAlign = ContentAlignment.MiddleCenter;
                panelResultado.Controls.Add(lblAlerta);
            }

            Button btnFinalizar = new Button();
            btnFinalizar.Text = "Finalizar Consulta";
            btnFinalizar.Font = new Font("Segoe UI", 12);
            btnFinalizar.Location = new Point(250, 280);
            btnFinalizar.Size = new Size(200, 45);
            btnFinalizar.BackColor = Color.White;
            btnFinalizar.Click += BtnFinalizar_Click;
            panelResultado.Controls.Add(btnFinalizar);
        }

        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            if (!sistema.RegistrosPorHospital.ContainsKey(hospital.Id))
            {
                sistema.RegistrosPorHospital[hospital.Id] = new List<RegistroMedico>();
            }
            sistema.RegistrosPorHospital[hospital.Id].Add(registro);

            paciente.Historial.Add(registro);

            if (!sistema.ColasPorHospital.ContainsKey(hospital.Id))
            {
                sistema.ColasPorHospital[hospital.Id] = new Queue<string>();
            }
            string clave = paciente.Id + "|" + registro.IdRegistro;
            sistema.ColasPorHospital[hospital.Id].Enqueue(clave);

            sistema.GuardarTodosDatos();

            string mensaje = $"═══════════════════════════════════════\n";
            mensaje += $"Consulta registrada exitosamente\n";
            mensaje += $"═══════════════════════════════════════\n\n";
            mensaje += $"ID de Registro: {registro.IdRegistro}\n";
            mensaje += $"Hospital: {hospital.Nombre}\n";
            mensaje += $"Diagnóstico Preliminar: {registro.Diagnostico}\n\n";
            mensaje += $"Un médico revisará su caso pronto\n";
            mensaje += $"Posición en cola: {sistema.ColasPorHospital[hospital.Id].Count}\n";

            MessageBox.Show(mensaje, "Consulta Completada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EmbalsesPRSteemitGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private String GetMonthName(DateTime dateAndTime)
        {
            string MonthName = string.Empty;
            if (dateAndTime.ToString("MM") == "01")
                MonthName = "enero";
            else if (dateAndTime.ToString("MM") == "02")
                MonthName = "febrero";
            else if (dateAndTime.ToString("MM") == "03")
                MonthName = "marzo";
            else if (dateAndTime.ToString("MM") == "04")
                MonthName = "abril";
            else if (dateAndTime.ToString("MM") == "05")
                MonthName = "mayo";
            else if (dateAndTime.ToString("MM") == "06")
                MonthName = "junio";
            else if (dateAndTime.ToString("MM") == "07")
                MonthName = "julio";
            else if (dateAndTime.ToString("MM") == "08")
                MonthName = "agosto";
            else if (dateAndTime.ToString("MM") == "09")
                MonthName = "septiembre";
            else if (dateAndTime.ToString("MM") == "10")
                MonthName = "octubre";
            else if (dateAndTime.ToString("MM") == "11")
                MonthName = "noviembre";
            else if (dateAndTime.ToString("MM") == "12")
                MonthName = "diciembre";
            return MonthName;
        }
        private ReservoirData GetReservoirLevel(string zoneID, int ArrayData)
        {
            string CurrentLevel = "";
            try
            {
                WebRequest request = WebRequest.Create("https://waterservices.usgs.gov/nwis/iv/?sites=" + zoneID.ToString() + "&period=P1D&format=json");
                System.Net.WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string ResponseFromServer = reader.ReadToEnd();
                JObject JsonDataResult = JObject.Parse(ResponseFromServer);
                IList<JToken> results = JsonDataResult["value"]["timeSeries"][ArrayData]["values"].Children().ToList();
                JToken LastItemInJson = results[results.Count() - 1];
                JToken Item1 = LastItemInJson["value"];
                JToken Item2 = Item1[Item1.Count() - 1];
                CurrentLevel = Item2.SelectToken("value").ToString();
                DateTime dateAndTime = DateTime.Parse(Item2.SelectToken("dateTime").ToString());
                string FullDate = dateAndTime.ToString("dd") + " de " + GetMonthName(dateAndTime) + " de " + dateAndTime.ToString("yyyy");
                string Time = dateAndTime.ToString("hh:mm tt");
                return new ReservoirData(CurrentLevel, FullDate, Time);
            }
            catch
            {
                return new ReservoirData("0.0", "none", "none");
            }
        }

        public class ReservoirData
        {
            private string CurrentLevel = string.Empty;
            private string date = string.Empty;
            private string time = string.Empty;
            public ReservoirData()
            {
                CurrentLevel = string.Empty;
                date = string.Empty;
                time = string.Empty;
            }
            public ReservoirData(string GetCurrentLevel, string getDate, string getTime)
            {
                CurrentLevel = GetCurrentLevel;
                date = getDate;
                time = getTime;
            }
            public string GetCurrentLevel()
            {
                return CurrentLevel;
            }
            public string getDate()
            {
                return date;
            }
            public string getTime()
            {
                return time;
            }
        }

        private String GetCaonillasAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 252)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 248)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 244)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 242)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 235)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetCariteAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 544)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 542)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 539)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 537)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 536)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetCarraizoAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 41.14)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 39.5)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 38.5)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 36.5)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 30)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetCerrillosAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 173.4)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 160)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 155.5)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 149.4)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 137.2)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetCidraAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 403.05)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 401.05)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 400.05)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 399.05)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 398.05)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetFajardoAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 52.5)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 48.3)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 43.4)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 37.5)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 26)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetGuajatacaAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 196)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 194)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 190)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 186)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 184)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetLaPlataAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 51)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 43)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 39)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 38)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 31)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetPatillasAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 67.07)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 66.16)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 64.33)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 60.52)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 59.45)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetRioBlancoAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 28.75)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 26.5)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 24.25)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 22.5)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 18)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private String GetToaVacaAlertLevel(string level)
        {
            String AlertLevel = "";
            if (Convert.ToDouble(level) >= 161)
            {
                AlertLevel = "Desborde";
            }
            else if (Convert.ToDouble(level) >= 152)
            {
                AlertLevel = "Seguridad";
            }
            else if (Convert.ToDouble(level) >= 145)
            {
                AlertLevel = "Observación";
            }
            else if (Convert.ToDouble(level) >= 139)
            {
                AlertLevel = "Ajuste";
            }
            else if (Convert.ToDouble(level) >= 133)
            {
                AlertLevel = "Control";
            }
            else
            {
                AlertLevel = "Fuera de Servicio";
            }
            return AlertLevel;
        }
        private void PublishReport(string filename, bool silent, DateTime date, bool PostOnly = false)
        {
            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create("https://api.steem.place/postToSteem/");
                request.Method = "POST";
                StreamReader accountFile = new StreamReader("account.txt");
                string currentline = "";
                string Account = "";
                string Key = "";
                while (accountFile.EndOfStream == false)
                {
                    currentline = accountFile.ReadLine();
                    if (currentline.Contains("account"))
                    {
                        string[] line = currentline.Split('=');
                        Account = line[1];
                    }
                    else if (currentline.Contains("key"))
                    {
                        string[] line = currentline.Split('=');
                        Key = line[1];
                    }
                }
                dynamic postData = "title=Reporte Embalses de Puerto Rico - " + date.ToString("dd") + " de " + GetMonthName(date) + " de " + date.ToString("yyyy - tt") + "&body=" + File.ReadAllText(filename) + "&author=" + Account + "&permlink=reporte-" + date.ToString("MM-dd-yyyy-tt").ToLower() + "&tags=puertorico,water,spanish,stats,castellano,estadisticas,agua,embalses,reservoirs,report,reporte&pk=" + Key;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                if (silent == false)
                    if (responseFromServer.Contains("ok"))
                        if (PostOnly == true)
                            MessageBox.Show("Report Posted");
                        else
                            MessageBox.Show("Report Generated and Posted");
                    else
                        if (responseFromServer.Contains("bandwidth limit exceeded"))
                        MessageBox.Show("error ocurred: Bandwidth Limit Exceeded");
                    else
                        MessageBox.Show("error ocurred: " + Environment.NewLine + responseFromServer);
            }
            catch
            {
                MessageBox.Show("error ocurred");
            }
        }
        private void GenerateReport(bool silent = false)
        {
            ReservoirData Caonillas = GetReservoirLevel("50026140", 1);
            ReservoirData Carite = GetReservoirLevel("50039995", 3);
            ReservoirData Carraizo = GetReservoirLevel("50059000", 2);
            ReservoirData Cerrillos = GetReservoirLevel("50113950", 2);
            ReservoirData Cidra = GetReservoirLevel("50047550", 2);
            ReservoirData Fajardo = GetReservoirLevel("50071225", 1);
            ReservoirData Guajataca = GetReservoirLevel("50010800", 1);
            ReservoirData LaPlata = GetReservoirLevel("50045000", 1);
            ReservoirData Patillas = GetReservoirLevel("50093045", 3);
            ReservoirData RioBlanco = GetReservoirLevel("50076800", 1);
            ReservoirData ToaVaca = GetReservoirLevel("50111210", 3);
            string FileName = "report-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-tt") + ".txt";
            StreamWriter WriteReport = new StreamWriter(FileName, false);
            WriteReport.WriteLine("Hola a todos," + Environment.NewLine);
            WriteReport.WriteLine("A continuación se muestran los niveles de agua de los embalses principales de Puerto Rico para el día de hoy. Reporte " + DateTime.Now.ToString("tt:") + Environment.NewLine);
            WriteReport.WriteLine("Reporte generado a las " + DateTime.Now.ToString("hh:mm tt"));
            WriteReport.WriteLine("# Caonillas");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50026140.jpg</center>");
            if (Convert.ToDouble(Caonillas.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Caonillas.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetCaonillasAlertLevel(Caonillas.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Caonillas.getTime());
            }
            WriteReport.WriteLine("# Carite");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50039995.jpg</center>");
            if (Convert.ToDouble(Carite.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Carite.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetCariteAlertLevel(Carite.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Carite.getTime());
            }
            WriteReport.WriteLine("# Carraízo");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50059000.jpg</center>");
            if (Convert.ToDouble(Carraizo.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Carraizo.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetCarraizoAlertLevel(Carraizo.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Carraizo.getTime());
            }
            WriteReport.WriteLine("# Cerrillos");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50113950.jpg</center>");
            if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Cerrillos.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetCerrillosAlertLevel(Cerrillos.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Cerrillos.getTime());
            }
            WriteReport.WriteLine("# Cidra");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50047550.jpg</center>");
            if (Convert.ToDouble(Cidra.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Cidra.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetCidraAlertLevel(Cidra.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Cidra.getTime());
            }
            WriteReport.WriteLine("# Fajardo");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50071225.jpg</center>");
            if (Convert.ToDouble(Fajardo.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Fajardo.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetFajardoAlertLevel(Fajardo.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Fajardo.getTime());
            }
            WriteReport.WriteLine("# Guajataca");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50010800.jpg</center>");
            if (Convert.ToDouble(Guajataca.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Guajataca.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetGuajatacaAlertLevel(Guajataca.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Guajataca.getTime());
            }
            WriteReport.WriteLine("# La Plata");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50045000.jpg</center>");
            if (Convert.ToDouble(LaPlata.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + LaPlata.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetLaPlataAlertLevel(LaPlata.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + LaPlata.getTime());
            }
            WriteReport.WriteLine("# Patillas");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50093045.jpg</center>");
            if (Convert.ToDouble(Patillas.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + Patillas.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetPatillasAlertLevel(Patillas.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + Patillas.getTime());
            }
            WriteReport.WriteLine("# Rio Blanco");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50076800.jpg</center>");
            if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + RioBlanco.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetRioBlancoAlertLevel(RioBlanco.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + RioBlanco.getTime());
            }
            WriteReport.WriteLine("# Toa Vaca");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50111210.jpg</center>");
            if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) == 0.0)
            {
                WriteReport.WriteLine("No se pudo obtener los datos de este embalse.");
            }
            else
            {
                WriteReport.WriteLine("Nivel: " + ToaVaca.GetCurrentLevel() + " metros");
                WriteReport.WriteLine("Nivel de Alerta: " + GetToaVacaAlertLevel(ToaVaca.GetCurrentLevel()));
                WriteReport.WriteLine("Hora de Lectura: " + ToaVaca.getTime() + Environment.NewLine);
            }
            WriteReport.WriteLine("-------------------------------------");
            WriteReport.WriteLine("Imágenes y datos recopilados del USGS (United States Geological Survey) https://usgs.gov" + Environment.NewLine + Environment.NewLine);
            WriteReport.WriteLine("-------------------------------------" + Environment.NewLine);
            WriteReport.WriteLine("¡Mantente al día de las condiciones de los embalses descargando el app \"Embalses de Puerto Rico\" disponible para Android!");
            WriteReport.WriteLine("https://play.google.com/store/apps/details?id=msc.app.embalsespuertorico" + Environment.NewLine);
            WriteReport.WriteLine("-------------------------------------");
            WriteReport.WriteLine("Este reporte fue generado por el programa de @moisesmcardona. Si este reporte te ha parecido informativo, considera votar a @moisesmcardona como Witness. [Lee más aquí sobre mi witness y como votar.](https://steemit.com/witness/@moisesmcardona/witness-espanol)");
            WriteReport.Close();
            PublishReport(FileName, silent, DateTime.Now);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] vars = Environment.GetCommandLineArgs();
            if (vars.Count() > 1)
            {
                if (vars[1] == "-s")
                {
                    GenerateReport(true);
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime textboxdate = DateTime.ParseExact(textBox1.Text, "yyyy-MM-dd-hh-mm-ss-tt", null);
                PublishReport("report-" + textboxdate.ToString("yyyy-MM-dd-hh-mm-ss-tt") + ".txt", false, textboxdate, true);
            }
            catch
            {
                MessageBox.Show("Date may be incorrect");
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace EmbalsesPRSteemitGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private ReservoirData GetReservoirLevelAsync(string zoneID, int ArrayData)
        {
            string CurrentLevel = "";
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
            string FullDate = dateAndTime.ToString("dd") + " de " + MonthName + " de " + dateAndTime.ToString("yyyy");
            string Time = dateAndTime.ToString("hh:mm tt");
            return new ReservoirData(CurrentLevel, FullDate, Time);
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

        private void button1_Click(object sender, EventArgs e)
        {
            ReservoirData Caonillas = GetReservoirLevelAsync("50026140", 1);
            ReservoirData Carite = GetReservoirLevelAsync("50039995", 3);
            ReservoirData Carraizo = GetReservoirLevelAsync("50059000", 2);
            ReservoirData Cerrillos = GetReservoirLevelAsync("50113950", 2);
            ReservoirData Cidra = GetReservoirLevelAsync("50047550", 2);
            ReservoirData Fajardo = GetReservoirLevelAsync("50071225", 1);
            ReservoirData Guajataca = GetReservoirLevelAsync("50010800", 1);
            ReservoirData LaPlata = GetReservoirLevelAsync("50045000", 1);
            ReservoirData Patillas = GetReservoirLevelAsync("50093045", 3);
            ReservoirData RioBlanco = GetReservoirLevelAsync("50076800", 1);
            ReservoirData ToaVaca = GetReservoirLevelAsync("50111210", 3);
            String CaonillasLevel = "", CariteLevel = "", CarraizoLevel = "", CerrillosLevel = "", CidraLevel = "", FajardoLevel = "", GuajatacaLevel = "", LaPlataLevel = "", PatillasLevel = "", RioBlancoLevel = "", ToaVacaLevel = "";
            //Caonillas
            if (Convert.ToDouble(Caonillas.GetCurrentLevel()) >= 252)
            {
                CaonillasLevel = "Desborde";
            }
            else if (Convert.ToDouble(Caonillas.GetCurrentLevel()) >= 248)
            {
                CaonillasLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Caonillas.GetCurrentLevel()) >= 244)
            {
                CaonillasLevel = "Observación";
            }
            else if (Convert.ToDouble(Caonillas.GetCurrentLevel()) >= 242)
            {
                CaonillasLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Caonillas.GetCurrentLevel()) >= 235)
            {
                CaonillasLevel = "Control";
            }
            else {
                CaonillasLevel = "Fuera de Servicio";
            }
            //Carite
            if (Convert.ToDouble(Carite.GetCurrentLevel()) >= 544)
            {
                CariteLevel = "Desborde";
            }
            else if (Convert.ToDouble(Carite.GetCurrentLevel()) >= 542)
            {
                CariteLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Carite.GetCurrentLevel()) >= 539)
            {
                CariteLevel = "Observación";
            }
            else if (Convert.ToDouble(Carite.GetCurrentLevel()) >= 537)
            {
                CariteLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Carite.GetCurrentLevel()) >= 536)
            {
                CariteLevel = "Control";
            }
            else
            {
                CariteLevel = "Fuera de Servicio";
            }
            //Carraizo
            if (Convert.ToDouble(Carraizo.GetCurrentLevel()) >= 41.14)
            {
                CarraizoLevel = "Desborde";
            }
            else if (Convert.ToDouble(Carraizo.GetCurrentLevel()) >= 39.5)
            {
                CarraizoLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Carraizo.GetCurrentLevel()) >= 38.5)
            {
                CarraizoLevel = "Observación";
            }
            else if (Convert.ToDouble(Carraizo.GetCurrentLevel()) >= 36.5)
            {
                CarraizoLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Carraizo.GetCurrentLevel()) >= 30)
            {
                CarraizoLevel = "Control";
            }
            else
            {
                CarraizoLevel = "Fuera de Servicio";
            }
            //Cerrillos
            if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) >= 173.4)
            {
                CerrillosLevel = "Desborde";
            }
            else if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) >= 160)
            {
                CerrillosLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) >= 155.5)
            {
                CerrillosLevel = "Observación";
            }
            else if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) >= 149.4)
            {
                CerrillosLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Cerrillos.GetCurrentLevel()) >= 137.2)
            {
                CerrillosLevel = "Control";
            }
            else
            {
                CerrillosLevel = "Fuera de Servicio";
            }
            //Cidra
            if (Convert.ToDouble(Cidra.GetCurrentLevel()) >= 403.05)
            {
                CidraLevel = "Desborde";
            }
            else if (Convert.ToDouble(Cidra.GetCurrentLevel()) >= 401.05)
            {
                CidraLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Cidra.GetCurrentLevel()) >= 400.05)
            {
                CidraLevel = "Observación";
            }
            else if (Convert.ToDouble(Cidra.GetCurrentLevel()) >= 399.05)
            {
                CidraLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Cidra.GetCurrentLevel()) >= 398.05)
            {
                CidraLevel = "Control";
            }
            else
            {
                CidraLevel = "Fuera de Servicio";
            }
            //Fajardo
            if (Convert.ToDouble(Fajardo.GetCurrentLevel()) >= 52.5)
            {
                FajardoLevel = "Desborde";
            }
            else if (Convert.ToDouble(Fajardo.GetCurrentLevel()) >= 48.3)
            {
                FajardoLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Fajardo.GetCurrentLevel()) >= 43.4)
            {
                FajardoLevel = "Observación";
            }
            else if (Convert.ToDouble(Fajardo.GetCurrentLevel()) >= 37.5)
            {
                FajardoLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Fajardo.GetCurrentLevel()) >= 26)
            {
                FajardoLevel = "Control";
            }
            else
            {
                FajardoLevel = "Fuera de Servicio";
            }
            //Guajataca
            if (Convert.ToDouble(Guajataca.GetCurrentLevel()) >= 196)
            {
                GuajatacaLevel = "Desborde";
            }
            else if (Convert.ToDouble(Guajataca.GetCurrentLevel()) >= 194)
            {
                GuajatacaLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Guajataca.GetCurrentLevel()) >= 190)
            {
                GuajatacaLevel = "Observación";
            }
            else if (Convert.ToDouble(Guajataca.GetCurrentLevel()) >= 186)
            {
                GuajatacaLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Guajataca.GetCurrentLevel()) >= 184)
            {
                GuajatacaLevel = "Control";
            }
            else
            {
                GuajatacaLevel = "Fuera de Servicio";
            }
            //La Plata
            if (Convert.ToDouble(LaPlata.GetCurrentLevel()) >= 51)
            {
                LaPlataLevel = "Desborde";
            }
            else if (Convert.ToDouble(LaPlata.GetCurrentLevel()) >= 43)
            {
                LaPlataLevel = "Seguridad";
            }
            else if (Convert.ToDouble(LaPlata.GetCurrentLevel()) >= 39)
            {
                LaPlataLevel = "Observación";
            }
            else if (Convert.ToDouble(LaPlata.GetCurrentLevel()) >= 38)
            {
                LaPlataLevel = "Ajuste";
            }
            else if (Convert.ToDouble(LaPlata.GetCurrentLevel()) >= 31)
            {
                LaPlataLevel = "Control";
            }
            else
            {
                LaPlataLevel = "Fuera de Servicio";
            }
            //La Plata
            if (Convert.ToDouble(Patillas.GetCurrentLevel()) >= 67.07)
            {
                PatillasLevel = "Desborde";
            }
            else if (Convert.ToDouble(Patillas.GetCurrentLevel()) >= 66.16)
            {
                PatillasLevel = "Seguridad";
            }
            else if (Convert.ToDouble(Patillas.GetCurrentLevel()) >= 64.33)
            {
                PatillasLevel = "Observación";
            }
            else if (Convert.ToDouble(Patillas.GetCurrentLevel()) >= 60.52)
            {
                PatillasLevel = "Ajuste";
            }
            else if (Convert.ToDouble(Patillas.GetCurrentLevel()) >= 59.45)
            {
                PatillasLevel = "Control";
            }
            else
            {
                PatillasLevel = "Fuera de Servicio";
            }
            //Rio Blanco
            if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) >= 28.75)
            {
                RioBlancoLevel = "Desborde";
            }
            else if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) >= 26.5)
            {
                RioBlancoLevel = "Seguridad";
            }
            else if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) >= 24.25)
            {
                RioBlancoLevel = "Observación";
            }
            else if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) >= 22.5)
            {
                RioBlancoLevel = "Ajuste";
            }
            else if (Convert.ToDouble(RioBlanco.GetCurrentLevel()) >= 18)
            {
                RioBlancoLevel = "Control";
            }
            else
            {
                RioBlancoLevel = "Fuera de Servicio";
            }
            //Toa Vaca
            if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) >= 161)
            {
                ToaVacaLevel = "Desborde";
            }
            else if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) >= 152)
            {
                ToaVacaLevel = "Seguridad";
            }
            else if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) >= 145)
            {
                ToaVacaLevel = "Observación";
            }
            else if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) >= 139)
            {
                ToaVacaLevel = "Ajuste";
            }
            else if (Convert.ToDouble(ToaVaca.GetCurrentLevel()) >= 133)
            {
                ToaVacaLevel = "Control";
            }
            else
            {
                ToaVacaLevel = "Fuera de Servicio";
            }
            StreamWriter WriteReport = new StreamWriter("report.txt", false);
            WriteReport.WriteLine("Hola a todos," + Environment.NewLine);
            WriteReport.WriteLine("A continuación se muestran los niveles de agua de los embalses principales de Puerto Rico para el día de hoy. Reporte " + DateTime.Now.ToString("tt:") + Environment.NewLine);
            WriteReport.WriteLine("Reporte generado a las " + DateTime.Now.ToString("hh:mm tt"));
            WriteReport.WriteLine("# Caonillas");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50026140.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Caonillas.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + CaonillasLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Caonillas.getTime());
            WriteReport.WriteLine("# Carite");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50039995.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Carite.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + CariteLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Carite.getTime());
            WriteReport.WriteLine("# Carraízo");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50059000.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Carraizo.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + CarraizoLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Carraizo.getTime());
            WriteReport.WriteLine("# Cerrillos");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50113950.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Cerrillos.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + CerrillosLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Cerrillos.getTime());
            WriteReport.WriteLine("# Cidra");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50047550.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Cidra.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + CidraLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Cidra.getTime());
            WriteReport.WriteLine("# Fajardo");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50071225.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Fajardo.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + FajardoLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Fajardo.getTime());
            WriteReport.WriteLine("# Guajataca");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50010800.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Guajataca.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + GuajatacaLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Guajataca.getTime());
            WriteReport.WriteLine("# La Plata");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50045000.jpg</center>");
            WriteReport.WriteLine("Nivel: " + LaPlata.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + LaPlataLevel);
            WriteReport.WriteLine("Hora de Lectura: " + LaPlata.getTime());
            WriteReport.WriteLine("# Patillas");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50093045.jpg</center>");
            WriteReport.WriteLine("Nivel: " + Patillas.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + PatillasLevel);
            WriteReport.WriteLine("Hora de Lectura: " + Patillas.getTime());
            WriteReport.WriteLine("# Rio Blanco");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50076800.jpg</center>");
            WriteReport.WriteLine("Nivel: " + RioBlanco.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + RioBlancoLevel);
            WriteReport.WriteLine("Hora de Lectura: " + RioBlanco.getTime());
            WriteReport.WriteLine("# Toa Vaca");
            WriteReport.WriteLine("<center>https://waterdata.usgs.gov/nwisweb/local/state/pr/text/pics/50111210.jpg</center>");
            WriteReport.WriteLine("Nivel: " + ToaVaca.GetCurrentLevel() + " metros");
            WriteReport.WriteLine("Nivel de Alerta: " + ToaVacaLevel);
            WriteReport.WriteLine("Hora de Lectura: " + ToaVaca.getTime() + Environment.NewLine);
            WriteReport.WriteLine("-------------------------------------");
            WriteReport.WriteLine("Imágenes y datos recopilados del UGSG (United States Geological Survey) https://usgs.gov" + Environment.NewLine + Environment.NewLine);
            WriteReport.WriteLine("-------------------------------------" + Environment.NewLine);
            WriteReport.WriteLine("¡Mantente al día de las condiciones de los embalses descargando el app \"Embalses de Puerto Rico\" disponible para Android!");
            WriteReport.WriteLine("https://play.google.com/store/apps/details?id=msc.app.embalsespuertorico" + Environment.NewLine);
            WriteReport.WriteLine("-------------------------------------");
            WriteReport.WriteLine("Este reporte fue generado por el bot de @moisesmcardona. Si este reporte te ha parecido informativo, considera votando a @moisesmcardona como Witness. [Lee más aquí sobre mi witness y como votar.](https://steemit.com/witness/@moisesmcardona/witness-espanol)");
            WriteReport.Close();
            MessageBox.Show("Report has been generated");
        }
    }
}

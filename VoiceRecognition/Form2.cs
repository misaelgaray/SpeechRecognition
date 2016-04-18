using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;


namespace VoiceRecognition
{
    public partial class Form2 : Form
    {
        SpeechRecognitionEngine recEngine;
        ABCClass abc;
        
        public Form2()
        {
            InitializeComponent();
            abc = new ABCClass();
            
            
        }

        public string getSQL(bool var)
        {
            string sql = "";
            if (var == true) sql = "select nombre from empleados";
     
            return sql;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            button1.Enabled = false;
            button2.Enabled = true;
            txt_sql.Text = "";
            txt_natural.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Recognition
            recEngine.RecognizeAsyncStop();
            button1.Enabled = true;
            button2.Enabled = false;
            txt_sql.Text = "";
            txt_natural.Text = "";
        }

        public void  metodDelet(){

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            recEngine = new SpeechRecognitionEngine();

            /*Reglas para Select*/
            SrgsRule selectRule = new SrgsRule("selectRule");
            SrgsOneOf selectList = new SrgsOneOf(new string[]{
                "selecciona",
                "traeme",
                "busca",
                "obten",
                "muestrame",
                "muestra",
                "imprime",
                "imprimir"
            });
            selectRule.Add(selectList);

            /*Reglas para numero de registros
             * todos, los = all *
             */
            SrgsRule allRule = new SrgsRule("allRule");
            SrgsOneOf allList = new SrgsOneOf(new string[] { 
                "los",
                "todos los"
            });
            allRule.Add(allList);

            /*nombre de la tabla*/
            SrgsRule tablaRule = new SrgsRule("tablaRule");
            SrgsOneOf tablaList = new SrgsOneOf(new string[]{
                "empleados"
            });
            tablaRule.Add(tablaList);
            SrgsRule whereRule = new SrgsRule("whereRule");
            SrgsOneOf whereList = new SrgsOneOf(new string[] { 
                "igual"
            });
            allRule.Add(allList);


            /*La referencia con una root ruke hace que que las reglas se unana y tengan que tener un 
             orden especifico, quitar o comentar la regla principal y quirar del la creacion del documenteo */
            /*Regla principal*/
            SrgsRule mainRule = new SrgsRule("mainRule");
            mainRule.Scope = SrgsRuleScope.Public;

            /*Unir los select con los all*/
            // Create the "Subject" and "Verb" rule references and add them to the SrgsDocument.
            SrgsRuleRef selectRef = new SrgsRuleRef(selectRule, "theSelect");
            mainRule.Add(selectRef);

            SrgsRuleRef allRef = new SrgsRuleRef(allRule, "theAll");
            mainRule.Add(allRef);

            SrgsRuleRef tablaRef = new SrgsRuleRef(tablaRule, "theTabla");
            mainRule.Add(tablaRef);

            //Creando el documento con las reglas
            SrgsDocument newDocumento = new SrgsDocument();
            newDocumento.Rules.Add(new SrgsRule[]{
                mainRule,
                selectRule,
                allRule,
                tablaRule
            });
            newDocumento.Root = mainRule;

            //Agragar gramatica al engine
            Grammar grammar = new Grammar(newDocumento,"mainRule");

            recEngine.LoadGrammar(grammar);

            //Crear XML
            System.Xml.XmlWriter writer =
            System.Xml.XmlWriter.Create("C:\\Users\\Misael\\Documents\\ejemplo.xml");
            newDocumento.WriteSrgs(writer);
            writer.Close();


            // Attach a handler for the SpeechRecognized event.
            recEngine.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);

            // Configure the input to the SpeechRecognitionEngine object.
            recEngine.SetInputToDefaultAudioDevice();

            // Start asynchronous recognition.
            recEngine.RecognizeAsync();
        }
        
        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {//Engine
            string result = e.Result.Text;
            string select = "", all = "", tabla = "";
            bool what = false;

           // if (e.Result != null)
           // {
                if (e.Result.Semantics != null && e.Result.Semantics.Count != 0)
                {
                    if (e.Result.Semantics.ContainsKey("theSelect"))
                    {
                        select = e.Result.Semantics["theSelect"].Value.ToString();
                        what = true;
                    }
                    //else MessageBox.Show("Error");

                    if (e.Result.Semantics.ContainsKey("theAll"))
                    {
                        all = e.Result.Semantics["theAll"].Value.ToString();
                        what = true;
                    }
                    //else MessageBox.Show("Error");

                    if (e.Result.Semantics.ContainsKey("theTabla"))
                    {
                        tabla = e.Result.Semantics["theTabla"].Value.ToString();
                        what = true;
                    }
                   // else MessageBox.Show("Error");
                    txt_todo.Text = result;
                   txt_natural.Text = String.Format("{0}\n{1}\n{2}",select,all,tabla);
                    txt_sql.Text = getSQL(what);
                    abc.startConn();

                    var emp = abc.getEmpleados(getSQL(what));
                    foreach (var empleado in emp)
                    {
                        list_emp.Items.Add(String.Format("{0}", empleado));
                    }
                }
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        { //Esta madre no sirve 
            MessageBox.Show("HAz hecho click");
        }

        private void list_emp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
using System.Speech.Synthesis;


namespace VoiceRecognition
{
    public partial class Form1 : Form
    {
        /*Speech Recognition*/
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        //Synteus
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_enable_Click(object sender, EventArgs e)
        {
            //REcognition
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btn_disable.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //REcognition
            Choices commands = new Choices();
            commands.Add(new string[] { "ivan", "imprime mi nombre", "habla el texto" });

            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);

            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

         void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "ivan":
                    PromptBuilder builder = new PromptBuilder();
                    builder.StartSentence();
                    builder.AppendText("Poquemon tipo Morro, Este poquemon habita en el Cetis 16.", PromptEmphasis.Reduced);
                    builder.EndSentence();

                    builder.AppendBreak(new TimeSpan(0,0,0,0,50));

                    builder.StartSentence();
                    builder.AppendText("pa' que quieres saber eso, jaja saludos", PromptEmphasis.Strong);
                    builder.EndSentence();

                    synthesizer.SpeakAsync(builder);
                    break;
                case "imprime mi nombre":
                    richTextBox1.Text += "\nMisael";
                    break;
                case "habla el texto":
                    synthesizer.SpeakAsync(richTextBox1.SelectedText);
                    break;
            }
        }

         private void btn_disable_Click(object sender, EventArgs e)
         {
             //Recognition
             recEngine.RecognizeAsyncStop();
             btn_disable.Enabled = false;
         }

         private void button1_Click(object sender, EventArgs e)
         {
             Form2 f = new Form2();
             f.Show();
         }
    }
}

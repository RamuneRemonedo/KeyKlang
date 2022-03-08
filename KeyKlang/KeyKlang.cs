using SharpDX.XAudio2;
using System.Media;
using System.Reflection;

namespace KeyKlang
{
    public partial class KeyKlang : Form
    {

        private static XAudio2 xaDevice = new XAudio2();

        public KeyKlang()
        {

        }

        KeyboardHook keyboardHook = new KeyboardHook();

        protected override void OnLoad(EventArgs e)
        {
            keyboardHook.KeyDownEvent += KeyboardHook_KeyDownEvent;
            keyboardHook.KeyUpEvent += KeyboardHook_KeyUpEvent;
            keyboardHook.Hook();
        }

        private void KeyboardHook_KeyDownEvent(object sender, KeyEventArg e)
        {
            KeysConverter kc = new KeysConverter();

            String downKey = kc.ConvertToString(e.KeyCode);

            Console.WriteLine(downKey);

            var assm = Assembly.GetExecutingAssembly();
            Stream stream = null;

            if (downKey == "Enter")
            {
                stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-confirm.wav");
               

            }
            else if (downKey == "Back" || downKey == "Del")
            {
                stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-delete.wav");
                

            }
            else if (downKey == "Left" || downKey == "Right")
            {
                stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-movement.wav");
               

            }
            else if (downKey != "Enter" && downKey != "Back" && downKey != "Del" && downKey != "Left" && downKey != "Right" && downKey != "LShiftKey" && downKey != "RShiftKey")
            {

                int r = new Random().Next(1, 4);

                switch (r)
                {
                    case 1:
                        stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-press-1.wav");
                        break;

                    case 2:
                        stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-press-2.wav");
                        break;
                        
                    case 3:
                        stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-press-3.wav");
                        break;

                    case 4:
                        stream = assm.GetManifestResourceStream("KeyKlang.Resources.key-press-4.wav");
                        break;
                }
            }
            if (stream != null)
            {
                SoundPlayer player = new SoundPlayer(stream);
                player.Play();
            }
        }

        private void KeyboardHook_KeyUpEvent(object sender, KeyEventArg e)
        {
            // キーが離されたときにやりたいこと
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            keyboardHook.UnHook();
        }
    }
}

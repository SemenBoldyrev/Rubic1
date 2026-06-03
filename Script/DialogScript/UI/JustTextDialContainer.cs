using Godot;
using Rubic1.Script.DialogScript.DataSets;
using Rubic1.Script.DialogScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.UI
{
    public partial class JustTextDialContainer : Control, IDialInterface
    {
        [Export] RichTextLabel DialLabel;
        [Export] float textSpeed = 0.02f;

        public event Action<int> SelectedChoiceId;
        public event Action AnimationEnded;
        public event Action<int, int> PartEnded;

        public override void _Process(double delta)
        {
            if (DialLabel.VisibleRatio > 0) DialLabel.VisibleRatio += textSpeed;
            else
            {
                ProcessMode = ProcessModeEnum.Disabled;
                AnimationEnded.Invoke();
            }
        }

        public void ClearDial()
        {
            DialLabel.Text = "";
        }

        public void ShowDial(DialPartData part, bool animation = true)
        {
            if (part.Dial != null) DialLabel.Text = part.Dial;
            this.Visible = true;
        }

        private void StartDialAnimation(bool start = true)
        {
            if (start)
            {
                DialLabel.VisibleRatio = 0f;
                if (ProcessMode == ProcessModeEnum.Disabled) ProcessMode = ProcessModeEnum.Pausable;
            }
            else
            {
                DialLabel.VisibleRatio = 1f;
            }
        }

        public void SkipDial()
        {
            StartDialAnimation(false);
        }

        public void MakeVisible(bool visible = true)
        {
            this.Visible = visible;
        }
    }
}

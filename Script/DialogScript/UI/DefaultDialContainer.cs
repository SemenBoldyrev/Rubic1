using Godot;
using Godot.Collections;
using Rubic1.Script.DialogScript.DataSets;
using Rubic1.Script.DialogScript.Interfaces;
using Rubic1.Script.Sets.KeySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic1.Script.DialogScript.UI
{
    public partial class DefaultDialContainer : Control, IDialInterface
    {
        // !!! Need to change visible ration on something else !!!

        [Export] TextureRect ImgContainer;
        [Export] RichTextLabel NameLabel;
        [Export] RichTextLabel DialLabel;
        [Export] BoxContainer ButtonContainer;
        //Name animation "NEXT"
        [Export] AnimationPlayer NextAnimationPlayer;

        [Export] float textSpeed = 0.5f;

        private float ActualTextSpeed => textSpeed / DialLabel.Text.Length;

        public event Action AnimationEnded;
        public event Action<int, int> PartEnded;

        private bool skipNextAnimation = false;
        private DialPartData dialPart;

        private enum dialStates
        {
            Inactive,
            Preparing,
            Reading,
            Waiting,
            Choice
        }
        private dialStates curDialState = dialStates.Inactive;

        public void ShowDial(DialPartData part, bool animation = true)
        {
            dialPart = part;

            StartNextAnimation(false);

            if (part.SpritePath != null) SetImg(part.SpritePath, part.SpriteSpan[0], part.SpriteSpan[1], part.SpriteDimension[0], part.SpriteDimension[0]);
            if (part.Name != null) SetDialName(part.Name);
            //skipNextAnimation = part.ChoiceNames.Count > 0;

            MakeVisible();
            DialLabel.VisibleRatio = 0f;
            if (part.Dial != null) SetDial(part.Dial);
        }

        public override void _Ready()
        {
            AnimationEnded += () =>
            {
                //its needs overload, so i used action function
                 StartNextAnimation();
            };
        }

        public override void _Input(InputEvent @event)
        {
            if (curDialState == dialStates.Waiting && @event.IsActionPressed(MainKeyNames.ACTION))
            {
                curDialState = dialStates.Preparing;
                PartEnded?.Invoke(dialPart.Next, dialPart.Event);
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (curDialState != dialStates.Reading) return;
            if (DialLabel.VisibleRatio < 1) DialLabel.VisibleRatio += ActualTextSpeed;
            else
            {
                curDialState = dialStates.Preparing;
                AnimationEnded?.Invoke();
            }
        }

        public override void _Process(double delta)
        {
            //if (DialLabel.VisibleRatio < 1) DialLabel.VisibleRatio += textSpeed;
            //else
            //{
            //    ProcessMode = ProcessModeEnum.Disabled;
            //    AnimationEnded.Invoke();
            //}
        }

        private void SetImg(string path, float x, float y, float w, float h)
        {
            if (path  == null)
            {
                ImgContainer.Texture = null;
                return;
            }
            AtlasTexture texture = new();
            texture.Atlas = GD.Load<CompressedTexture2D>(path);
            texture.Region = new Rect2(x,y,w,h);
            ImgContainer.Texture = texture;
        }

        private void SetDialName(string name)
        {
            NameLabel.Text = name;
        }

        private void SetDial(string dial)
        {
            if (dial.Trim() == "")
            {
                DialLabel.Text = dial;
                return;
            }
            curDialState = dialStates.Reading;
            DialLabel.Text = dial;
            StartDialAnimation();
        }

        private void ShowChoice(DialPartData data)
        {
            for (int i = 0; i < data.ChoiceNames.Count; i++)
            {
                int index = i;
                Button btn = new Button();
                btn.Text = data.ChoiceNames[index];
                btn.Pressed += () => {
                    curDialState = dialStates.Preparing;
                    PartEnded?.Invoke(data.ChoiceNext[index], data.ChoiceEvent[index]); 
                };
                ButtonContainer.AddChild(btn);
            }
        }

        private void ClearButtons()
        {
            Array<Node> children = ButtonContainer.GetChildren();
            foreach (Node node in children) 
            {
                    node.QueueFree();
            }
        }

        private void StartNextAnimation(bool start = true)
        {
            if (start)
            {
                if (dialPart.ChoiceNames.Count > 0)
                {
                    ShowChoice(dialPart);
                    curDialState = dialStates.Choice;
                }
                else
                {
                    NextAnimationPlayer.Play("NEXT");
                    curDialState = dialStates.Waiting;
                }
            }
            else
            {
                NextAnimationPlayer.Play("RESET");
            }
        }

        private void StartDialAnimation(bool start = true)
        {
            if (start)
            {
                DialLabel.VisibleRatio = 0f;
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

        public void ClearDial()
        {
            // Clear setDial will sent the end signal on next part, so there should be a way to prevent it from happening [?]
            SetImg(null,0,0,0,0);
            SetDialName("");
            SetDial("");
            ClearButtons();
        }

        public void MakeVisible(bool visible = true)
        {
            this.Visible = visible;
            curDialState = visible ? dialStates.Preparing : dialStates.Inactive;
            ProcessMode = visible ? ProcessModeEnum.Pausable : ProcessModeEnum.Disabled;
        }
    }
}

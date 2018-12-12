using System;
using System.Collections.Generic;
using TikTakToe.GameManage;
using UIKit;

namespace TikTakToe
{
    public partial class ViewController : UIViewController
    {
        private IGameObjectManager gameObjectManager = null;
        private List<UIButton> _uIButtons = new List<UIButton>();

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            gameObjectManager = new GameObjectManager();

            gameObjectManager.WinAction += (bool obj) =>
            {
                BeginInvokeOnMainThread(() =>
                {
                    if (obj)
                    {
                        FinishGame();
                        lblStatus.Text = "Yes";
                    }
                    else
                        lblStatus.Text = "Try More";
                });

            };

            btnReset.TouchUpInside += (object sender, EventArgs e) =>
            {
                ResetUI();
            };

            _uIButtons.Add(btn1);
            _uIButtons.Add(btn2);
            _uIButtons.Add(btn3);
            _uIButtons.Add(btn4);
            _uIButtons.Add(btn5);
            _uIButtons.Add(btn6);
            _uIButtons.Add(btn7);
            _uIButtons.Add(btn8);
            _uIButtons.Add(btn9);

            InitiliazeGameUI(true);

            btn1.TouchUpInside += (sender, e) =>
            {
                ExecuteButtonProcess(btn1, 0);
            };

            btn2.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(1, btn2);
                btn2.Enabled = false;
            };

            btn3.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(2, btn3);
                btn3.Enabled = false;
            };

            btn4.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(3, btn4);
                btn4.Enabled = false;
            };

            btn5.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(4, btn5);
                btn5.Enabled = false;
            };


            btn6.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(5, btn6);
                btn6.Enabled = false;
            };


            btn7.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(6, btn7);
                btn7.Enabled = false;
            };

            btn8.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(7, btn8);
                btn8.Enabled = false;
            };

            btn9.TouchUpInside += (sender, e) =>
            {
                ManageAddingGameObject(8, btn9);
                btn9.Enabled = false;
            };

            meSelector.ValueChanged += (sender, e) =>
            {
                //InitiliazeGameUI(true);
            };

          


            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void ExecuteButtonProcess(UIButton btn, short position)
        {
            ManageAddingGameObject(position, btn);
            btn.Enabled = false;
        }

        private void SetWinningUI(List<int> buttonsIds)
        {
            foreach (var item in buttonsIds)
            {
                switch (item)
                {
                    case 0:
                        btn1.BackgroundColor = UIColor.Red;  
                        break;
                    case 1:
                        btn2.BackgroundColor = UIColor.Red;
                        break;
                    case 2:
                        btn3.BackgroundColor = UIColor.Red;
                        break;
                    case 3:
                        btn4.BackgroundColor = UIColor.Red;
                        break;
                    case 4:
                        btn5.BackgroundColor = UIColor.Red;
                        break;
                    case 5:
                        btn6.BackgroundColor = UIColor.Red;
                        break;
                    case 6:
                        btn7.BackgroundColor = UIColor.Red;
                        break;
                    case 7:
                        btn8.BackgroundColor = UIColor.Red;
                        break;
                    case 8:
                        btn9.BackgroundColor = UIColor.Red;
                        break;
                }
            }
        }

        private void FinishGame()
        {
            foreach (var item in _uIButtons)
            {
                item.Enabled = false;
            }
        }

        private void InitiliazeGameUI(bool buttonsEnable)
        {
            foreach (var item in _uIButtons)
            {
                item.Enabled = buttonsEnable;
                item.SetTitle("", UIControlState.Normal);
                item.BackgroundColor = UIColor.Gray;
            }

            lblStatus.Text = string.Empty;
        }

        private void ResetUI()
        {
            meSelector.Enabled = true;
            meSelector.ControlStyle = UISegmentedControlStyle.Bezeled;

            InitiliazeGameUI(true);
            gameObjectManager.Initiliaze();
        }

        private void ManageAddingGameObject(short coordinateNo, UIButton button)
        {
            GameObject gameObject = null;

            if (meSelector.SelectedSegment == 0)
                gameObject = new XGameObject(coordinateNo);
            else
                gameObject = new OGameObject(coordinateNo);
          

            gameObjectManager.AddGameObjectInGame(gameObject);

            SetGraphics(gameObject, button);

            gameObjectManager.CalculateGameCondition();
        }

        private void SetGraphics(GameObject gameObject, UIButton button)
        {
            button.SetTitle(gameObject.AssetText, UIControlState.Normal);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

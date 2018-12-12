using System;
namespace TikTakToe.GameManage
{
    public class XGameObject : GameObject
    {
       
        public XGameObject(short coordinateNo) : base(coordinateNo)
        {
            AssetText = "X";
            GameObjectCode = 1;
        }

    }
}

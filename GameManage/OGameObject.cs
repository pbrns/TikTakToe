using System;
namespace TikTakToe.GameManage
{
    public class OGameObject : GameObject
    {
        public OGameObject(short coordinateNo) : base(coordinateNo)
        {
            AssetText = "O";
            GameObjectCode = 2;
        }

    }
}

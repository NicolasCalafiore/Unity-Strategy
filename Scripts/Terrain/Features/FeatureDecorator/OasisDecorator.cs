namespace Terrain{


    public class OasisDecorator : TileDecorator {
        public OasisDecorator(HexTile tile) : base(tile) {
            this.tile.food += 1;
        }

    }


}
namespace Terrain{


    public class StoneDecorator : TileDecorator {
        public StoneDecorator(HexTile tile) : base(tile) {
            this.tile.construction += 2;
            this.tile.defense += 2;
        }

    }


}
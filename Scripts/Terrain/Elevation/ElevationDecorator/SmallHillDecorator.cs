namespace Terrain{


    public class  SmallHillDecorator: TileDecorator {
        public SmallHillDecorator(HexTile tile) : base(tile) {
            this.tile.production += 1;
        }

    }


}
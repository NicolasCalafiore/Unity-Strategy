namespace Terrain{


    public class ForestDecorator : TileDecorator {
        public ForestDecorator(HexTile tile) : base(tile) {
            this.tile.MovementCost *= 1.75f; 
            this.tile.production += 1;
            this.tile.food += 1;
        }

    }
}
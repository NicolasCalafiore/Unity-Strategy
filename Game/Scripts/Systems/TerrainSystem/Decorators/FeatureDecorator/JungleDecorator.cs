namespace Terrain{


    public class JungleDecorator : TileDecorator {
        public JungleDecorator(HexTile tile) : base(tile) {
            this.tile.MovementCost *= 2; // For example, doubling the movement cost
            this.tile.nourishment  += 2;
            this.tile.defense += 3;
        }

    }


}
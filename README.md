# HexProcGen

Procedural hex-grid world generation in Unity. The terrain pipeline builds water, regions/biomes, elevation, natural features, and resources from layered Perlin noise plus rule-based passes. A lightweight faction simulation exists, but terrain generation is the main focus.

![FullMap](https://github.com/user-attachments/assets/6432fab1-17b2-4a8b-b0eb-cee1d4b66ff0)

**Status**
Archived on August 15, 2024. I paused development to focus on my academics, and to study state-machine architecture after the AI behavior methods became hard to scale.

**Terrain Features**
- Hex-grid map generation with layered noise and deterministic passes.
- Water + land separation with oceans, rivers, and shoreline cleanup.
- Biome regions (e.g., plains, desert, grassland, highland, jungle, swamp, tundra).
- Elevation grouping (hills, mountains, valleys, canyons).
- Region-aware natural features and resource placement.
- Continent and region detection via flood-fill.
- Tile decorators that add gameplay stats (movement cost, defense, construction, etc.).

**Supporting Systems (Secondary)**
- Player/faction generation with government types and procedurally generated names.
- Character traits that influence diplomacy and relationships.
- Fog of war and basic time tick.

**Terrain Pipeline (High-Level)**
- Generate water maps (ocean/river), then derive regions/biomes.
- Generate elevation, then natural features and resources by region.
- Build hex tiles and apply decorators.
- Generate shores, territories, and final tile stats.

**Tuning Knobs**
- Map size and strategy enums live in `Game/Scripts/Core/MapManager.cs`.
- Player count lives in `Game/Scripts/Systems/PlayerSystem/PlayerManager.cs`.
- Feature and resource chances live in `Game/Scripts/Systems/TerrainSystem/Features/RegionSpecificRandom.cs` and `Game/Scripts/Systems/TerrainSystem/Resource/ResourceRandom.cs`.
- Name data lives in `Game/Resources/Data`.

**How To Run**
- Prerequisite: Unity (2021.x or later recommended).
- Open the project in Unity.
- Open a scene and press Play (e.g., `Game/Misc/Scenes/SampleScene.unity`).

**Screenshots**
![PerlinNoiseMaps](https://github.com/user-attachments/assets/58b74619-1e49-4129-8684-7ef124ba3623)
![relationshipsandpriorities](https://github.com/user-attachments/assets/4bc53494-edb4-44aa-9055-9f6686f7b700)
**What I Learned**
- Layered Perlin noise + rule-based passes can produce coherent hex-based worlds.
- Modular systems (terrain, players, diplomacy, cities) help keep Unity projects navigable.
- AI behavior scales poorly without clear architecture, motivating a shift to state machines and other datastructures

**License**
No license specified.







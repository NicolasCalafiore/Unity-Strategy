

 HexProcGen                                                                                                                        
    ![FullMap](https://github.com/user-attachments/assets/6432fab1-17b2-4a8b-b0eb-cee1d4b66ff0)                                                                                                                                
  Project Description                                                                                                               
                                                                                                                                    
  A Unity-based procedural world generation system that creates hexagonal grid maps using layered Perlin noise algorithms. The      
  system generates terrain features, continents, regions, and resources, with a player simulation layer where randomly generated    
  faction leaders possess distinct characteristics that influence diplomatic relationships.    
  
        ![IMG_0937](https://github.com/user-attachments/assets/58b74619-1e49-4129-8684-7ef124ba3623)
        
  Features:                                                                                                                         
  - Hex Grid Terrain - Procedurally generated maps using Perlin noise                                                               
  - Layered Noise Maps - Temperature, rainfall, resources, and features combined to create diverse biomes                           
  - Player System - Randomly generated leaders with government types and character traits                                           
  - Diplomacy System - Leader personalities dynamically affect inter-faction relationships                                          
  - Procedural Naming - Names generated based on government type and character attributes                                           
  - Fog of War - Visibility system for exploration                                                                                  
  - Time System - In-game time progression                                                                                          
                                                                                                                                    
  Core Systems:                                                                                                                     
  - TerrainSystem                                                                                                                   
  - CharacterSystem                                                                                                                 
  - PlayerSystem                                                                                                                    
  - DiplomacySystem                                                                                                                 
  - GovernmentSystem                                                                                                                
  - CitiesSystem                                                                                                                    
  - FogSystem                                                                                                                       
  - TimeSystem                                                                                                                      
                                                                                                                                    
  Status: Discontinued - Paused development to study state-machine architecture and address scalability challenges encountered      
  during AI behavior implementation.                                                                                                
                                                                                                                                    
  Screenshots                                                                                                                       
                                                                                                                                    
  Coming soon                                                                                                                       
                                                                                                                                    
  How to Run It                                                                                                                     
                                                                                                                                    
  Prerequisites                                                                                                                     
                                                                                                                                    
  - Unity (2021.x or later recommended)                                                                                             
                                                                                                                                    
  Setup                                                                                                                             
                                                                                                                                    
  1. Clone the repository                                                                                                           
  2. Open the project in Unity                                                                                                      
  3. Open the main scene and press Play                                                                                             
                                                                                                                                    
  Technologies Used                                                                                                                 
                                                                                                                                    
  - C#                                                                                                                              
  - Unity                                                                                                                           
  - ShaderLab                                                                                                                       
  - HLSL                                                                                                                            
                                                                                                                                    
  What I Learned                                                                                                                    
                                                                                                                                    
  - Procedural Generation - Implementing terrain generation through layered Perlin noise maps and combining ensemble 2D lists to    
  create diverse, coherent world features                                                                                           
  - Unity Game Architecture - Structuring a large-scale Unity project with modular, separated systems (Terrain, Player, Diplomacy,  
  etc.)                                                                                                                             
  - AI Systems & Architecture Challenges - Exploring OOP architecture principles and design patterns for AI behavior, and           
  recognizing when methods become unmanageable at scale—leading to the decision to study state-machine architecture for better      
  scalability                                                                                                                       
                                          







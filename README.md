# Procedurally Generated Terrain
This project demonstrates procedural generation through a terrain (mesh) made in Unity, using Perlin Noise. Procedural generation is a method of algorithmically creating data, that combines human-generated assets and computer-generated pseudo-randomness.

# Perlin Noise
Perlin noise is a gradient noise that generates a coherent pseudo-random visual pattern. It was developed by Ken Perlin in 1983, to produce natural appearing textures in CGI for the sci-fi film *Tron*. Since all the details in Perlin noise are of the same size, multiple scaled copies can be used to create a variety of procedural textures.

*Perlin Noise:*<br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/1_vs239SecVBaB4HvLsZ8O5Q.png" width="200"><br/>
*Random Noise:*<br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/08b27199a1ddac58ab63126e37afd365ed9a9e5b/Demo%20Images/1_H6lwuHlprj1GYqRmav_Y2A.png" width="200"><br/>

The following mesh was made by generating the Y co-ordinate using the PerlinNoise function from Unity’s Scripting API (Shaded using Unity's Universal Rendering Pipeline). <br/>

- *(75x75) vertex Mesh with 1 layer of Perlin Noise:* <br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/Perlin.PNG" width="500"><br/>

# Octaves
To produce more realistic surfaces, multiple layers of Perlin Noise, (know as ‘octaves’) are used. Different parameters such as Scale, Octaves, Lacunarity and Persistence can be used to control the appearance of the mesh. For example, here are some (75x75) vertex meshes with different numbers of octaves: <br/>
(Parameters: Scale= 26, Lacunarity= 0.5, Persistence= 2)

- **1 octave:** <br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/1.PNG" width="400"><br/>

- **2 octaves:** <br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/2.PNG" width="400"><br/>

- **3 octaves:** <br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/3.PNG" width="400"><br/>

- **4 octaves:** <br/>
<img src="https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/4.PNG" width="400"><br/>

# Terrains and Colour Maps
A simple terrain can be obtained from these meshes by applying a Colour Map according to the vertex height (Y-Coordinate). Naturally, using a higher number of vertices in the mesh would produce a more realistic landscape. For the purposes of this demo, a (75x75) vertex mesh was used.
- *Procedurally Generated Terrain* <br/>
 ![FW 21 t-SNE](https://github.com/Aadit3003/Procedurally-Generated-Mesh/blob/91f311ccbc27a468bb44cd03acfb859d1bde4556/Demo%20Images/Animation.gif)

# Applications
Procedural generation is a branch of media synthesis and is often used to create textures and 3D models. Some of its modern applications include:
### Open-World Video Games
Open-world games use procedural generation systems to create their pixel or voxel-based biomes. The most well-known example of this is **Minecraft**, which allows the player to also adjust generation parameters. Additionally, **No Man's Sky** features a universe with 18 quintillion procedurally generated planets, each of which is regenerated using a single random seed number in a deterministic engine.
### CGI in Films
Procedural generation is used to rapidly create visually interesting and accurate CGI. Recently, it was used in **The Mandalorian** which used the modified Unreal Engine 4 to create gorgeous alien landscapes. It was also used in **The Lord of the Rings** trilogy for crowd-related visual effects such as giant armies.

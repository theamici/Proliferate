# Agent of Change - Design Document

## Intro
The solution is built around the 3 simple underlying systems, which together comprise the foundations of a game expertly capable of mixing an interesting narrative with a complex dynamic world. The systems are:

- The C# Game System,
- The Ink Story System,
- and The Unity UI System

When implementing Proliferate in a brand new Unity project, this is what you'll have to expect to do to make a playable game with all expected basic features:

1. You will have to provide graphics (images), csv files (for things like name/title texts), and implement any custom animations.
2. You'll also have to write the code that create edges and give attribute data to nodes (the NodeSetupHandlers), and the code for the passive behaviours of nodes that are created, terminated or ticking/updating through time in the game (the ManipulableHandlers). 
3. Lastly, you'll have to write the Regular Moments and Special Moments in Ink that let you inspect nodes and make decisions on nodes, or progress the story with special storytelling and special decisions.

You may also have to decorate the GUI just to make the game more stylish, cool, or whatever experience you are going for, and add things like main menu and in-game menu for exiting the game and starting new games. Methods for saving and loading the game are included, so you can easily call these on a "Save Game" or "Load Game" button.

## C# Game System

### Nodes and Edges

The game is built around "nodes" and "edges". A node represents an object or thing, while an "edge" represents a one-directional connection or one-directional relationship. Thus, a node can be for instance a person, a house, or a country. While an edge would be friendship, rental agreement, or citizenship.

As an example: a "person" (node A) could "be a citizen" (edge B) of a "country" (node C). Of course, in this case of a relationship, we'll need 2 edges, because we have to say both that a country is related to a person, and that a person is related to a country, both sides of the relationship are likely interesting to us. However, this is not always the case. For instance, a "person" (node A) could be "infatuated" (edge B) with another "person" (node C), or a person drives a car. In the latter case, it's better to have the car be a passive and simple target which a person uses (drives), and therefore we only need one edge, that of a person to a car, the edge being of the kind "drives".

In the game, you spawn nodes, and the nodes then spawn edges. Although, of course, nodes can also spawn new nodes, which can be handy if you want to implement a feature for human procreation, or a machine (node A) making items (nodes B, C, D...). Using your written behaviours (automatic actions: runs while time passes, and upon node/edge creation or termination), decisions (active actions: player selections or AI selections), or node setup (NodeSetupHandler), any manually spawned nodes will after coming into being start spawning new nodes and new edges (and of course, also despawn when conditions are right) by themselves forever, creating a dynamic world of evolving complexity.

## Ink Story System

TODO

## Unity UI System

TODO

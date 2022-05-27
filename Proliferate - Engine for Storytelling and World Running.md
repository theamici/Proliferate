# Agent of Change - Design Document

- C# Game System
- Ink Story System
- Unity UI System

## C# Game System

### Nodes and Edges

ProliferateNode and ProfilerateEdge are the fundamental building blocks of a Proliferate game system, and both inherit from ProliferateObject.

ProliferateManipulable:
- kindID, an kind id integer
- constantAttributes, a dictionary of integers accessed by corresponding attribute id integers, the integers count the number of sources like nodes and edges that are currently enabling the attribute, with number increasing when new sources are created, and decreasing when old sources are terminated, if the number reaches 0 then the attribute itself is terminated
- discreteAttributes, a dictionary of integers accessed by corresponding attribute id integers
- continuousAttributes, a dictionary of floats accessed by corresponding attribute id integers

ProliferateNode:
- nodeID, an integer that uniquely identifies the node among all others
- nameRefs, an integer array where each indexed number maps to a name in a catalogue of names, each catalogue being given based on the node kind id and the index position
- depictionRefs, an integer array where each indexed number maps to a graphic in a catalogue of graphics
- edges, a dictionary of lists of edges, with the lists accessed by their common edge kind id

ProliferateEdge:
- targets, a list of the nodes targeted by this edge

Each kind id for nodes and edges are further distinctly associated with a ProliferateBehaviour. The latter act as shared sources of behaviour for nodes or edges of the same kind.

ProliferateBehaviour:
- OnCreation(nodeInContext, edgeInContext), when a node or edge is created of the given kind, this method runs, taking the node and the edge of the given context as its parameters, and using their data to find related nodes/edges, discover if conditions are met, and then make changes by adding, subtracting or transforming either nodes or edges related to the node or edge
- OnTimePassing(nodeInContext, edgeInContext), same as OnCreation, but is ran every step when time is passing in the game
- OnTermination(nodeInContext, edgeInContext), same as OnCreation, but is ran only when the node or edge is to be terminated

### Controllers for AI and player

Decision-making, and thus controllers for AIs and players are handled in the game through the concept of "piloting". That is, a derived class taking control of an underlying class, the underlying being the ProliferateNode. On top of this class is built the ProliferatePilotedNode, which is then used by either a ProliferateAIPilot which can control multiple nodes individually as AI characters, or a ProliferatePlayerPilot, which allows players to take control of nodes. In either case, control is exercised through ProliferateOption that depend on a ProliferateDecision

ProliferatePilotedNode:
- Options, a list of ProliferateOption sorted by their MotivationScore, it's worth noting that an option's MotivationScore will influence the quality of how the option executes
- ExecutionCountdown, a countdown to when the next option can be made because some other option is being carried out. It's worth noting 2 things here: 1) that sometimes there will be no good decisions to carry out, and then the option will default to a Procrastination option that does nothing but add to the countdown, and 2) if an ProliferateOption with a lower MotivationScore is selected by either the ProliferateAIPilot or by the player in the UI, then the option may extend the execution time, and vice-versa if the MotivationScore is high

ProliferateAIPilot:
- PilotedNodes, a list of all nodes piloted by this AI pilot
- OnPiloting(pilotedNode), if no other event handler is provided, then by default an option is semi-randomly executed from among the top highest scoring decisions

ProliferatePlayerPilot:
- PilotedNode, the node piloted by the player
- MakeDecision(option), takes an option selected in the UI and executes it

ProliferateOption:
- KindID, an kind id integer
- MotivationScore, an integer that measures the motivation for this option
- TotalEnablers, an integer that counts the number of sources like nodes and edges that are currently enabling this, the number increasing when new sources are added or lowering when old sources are terminated, if the number reaches 0 then the option itself is terminated

ProliferateDecision:
- KindID, the kind id integer
- OnScoringMotivation(option, pilotedNode), which takes an option of the given kind and calculates and sets the MotivationScore for it, using the data of the piloted node it belongs to
- OnDecisionExecution(option, pilotedNode), which takes an option of the given kind and the piloted node it belongs to, from the former it may use the MotivationScore and/or TotalEnablers to shape execution, while from the latter it uses its data to find related nodes/edges, discover if conditions are met, and then make changes by adding, subtracting or transforming either nodes or edges related to the piloted node

### Hub, Names, Descriptions and Depictions

Related to what's been covered so far, there are also 3 general nodes, namely the ProliferateDescriber, ProliferateNamer, and ProliferateDepicter.

ProliferateDescriber has only a single overloaded public method:
- InsertLinkedDescription(edgeKind, nodeInContext, edgeInContext, UIText), which perform the simple task of accessing descriptions for edge kinds written in Ink, changing contextually dependent references in the Ink text to the names of referred nodes found in the nodeInContext and/or edgeInContext, and lastly adding the text to the given UI element while also making those names clickable links
- InsertLinkedDescription(nodeKind, nodeInContext, UIText), does the same, but for descriptions of node kinds in Ink instead

ProliferateNamer has 3 public methods:
- GetName(reference, index, kindID), the kindID and index are combined to find the right name catalogue, and from this a distinct name, using the reference, is returned
- PickNewName(node, namePickingDelegate), uses the namePickingDelegate to carefully change the existing Name_Refs through extraordinary means
- NameNewNode(node), uses information about a node to give it a new set of names

ProliferateDepicter works the same way as the ProliferateNamer, except instead of names it finds and chooses graphics. If resources allow, a simplified version of setting and retrieving graphics will also be made for Proliferateedges (though they won't be named, because naming is only for nodes).

ProliferateHub is a general node that registers (by means of hashed text) and allows access to the kind id integers, ProliferateBehaviours and ProliferateDecisions. ProliferateHub also contains the ProliferateNamer, the ProliferateDescriber and the ProliferateDepicter. Furthermore, all nodes and edges are created via the hub and stored in 2 lists there, and so too all AI pilots and the player pilot. When a new node is created, it requires at the same time registration with an AI pilot (unless player pilot is specified) and a behaviour. When a new edge is created, it requires registration with a behaviour.

## Ink Story System

The story is divided up into separate Moments. A Moment can be a RegularMoment or a SpecialMoment.

In a RegulerMoment, the player is just viewing information related to some node or edge, and making decisions available for the node or edge. That is, they are given a standard set of interactions.

In a SpecialMoment however, the player gains access to an extraordinary set of interactions for a node or edge where they can request additional information and perform additional special decisions that progress the story of the game. What information and options are available may depend on the state of the game system, which is retrieved by Ink using external functions.

ProliferateInkManager contains method bindings to ink for playing audio, setting depictions, playing animations, retrieving game system data and executing game system decisions, etc.

## Unity UI System

TODO
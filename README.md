# Tilt Game

**Input:** Field n*n, m balls with different coordinates and m holes with different coordinates are on field. Each ball has ID and a hole with same ID. 

**Conditions:** Field can be tilted in one of four directions: north, south, east, west. After the tilt ball moves in corresponding direction until it hits the wall, hits another ball or fall into the hole. Hole counts as occupied after ball falls into it, second ball moves above the hole.

**Win condition:** All balls fell into holes with corresponding id.

**Fail condition:** Ball fell into the wrong hole or there's no moves to send ball into correct hole left.

**Goal:** Find the winning strategy or check that this strategy does not exist.

**Data input:** On the first run file "SetFieldHere.json" is created in application directory with default field state. It should be used for setting the field for all next runs.



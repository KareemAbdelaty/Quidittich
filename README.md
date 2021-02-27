# Quidittich Simulator
This project simulates a simple version of the quidditch game that features in the Harry Potter series.
The game's rules are simple. There are 2 teams ("griffindor and "slythrin") and one snitch inside the stadium. The 2 teams start at
the opposite ends of the stadium and once the game starts, all players will attempt to capture the snitch. Each catch of the snitch 
awards the catching player's team a point, succesive catches award 2 points. The first team to 100 points win.

#Game narrative
##Entities
There are 2 types of Entities inside the game
**Snitch:**
![image](https://user-images.githubusercontent.com/69083495/109401072-3e56f780-7955-11eb-9d74-3efabdccbb5d.png)
![snitch](https://user-images.githubusercontent.com/69083495/109401285-71e65180-7956-11eb-9f71-db7834c24068.gif)
**Snitch Behaviour**
During the game, the snitch will choose a random point inside the stadium and start travelling towards it. If it reaches its destination the snitch will choose another random point to move towards. As a magical being the snitch is able to fly and ignore gravity. Finally, if a wizard gets too close to the snitch and catches it, the snitch will award the wizard's team a point and then escapes by teleporting itself away.

**Wizard**
![image](https://user-images.githubusercontent.com/69083495/109402033-297d6280-795b-11eb-9987-f71740bb8479.png)
![image](https://user-images.githubusercontent.com/69083495/109402009-0488ef80-795b-11eb-8462-de1a3b27881b.png)
![green](https://user-images.githubusercontent.com/69083495/109402054-4dd93f00-795b-11eb-9531-77e5a0fc78b0.gif)
Not all wizards are created equal. Wizards have a wide range of traits that affect how they preform in game. Here is a list of these traits:
Flying Ability: some wizards are more confident in their flying abilities than their peers. This allows them to reach a higher maximum velocity.
Mind Control and Resistance: some wizards are skilled in mind magic. Them allows them to trick enemies that come close enough into helping them or to resist getting their mind controlled. All wizards can shake off mind control after a while.
Invlunerablility : Some wizards are able to avoid getting knocked out by making themselves invulnerable. This is hard to do so they are not always successful
Fitness: Flying is hard,as such wizards can get tired after flying for a long time. If a wizard is tired they will levitate in place to rest
**Wizard Behaviour**
During the game each wizard will look for the snitch and then start flying after it in order catch it while also trying to avoid colliding with the other wizards or the ground. . Although wizards try their best to not collide with each other or with the ground, sometimes collsions are inevitable. If a wizard collides with the ground they are knocked out for 20 seconds. When they wake up they have to make their way to their team's starting postion before they can rejoin the game. If a wizard collides with a wizard from another team the more aggressive wizard will knock the other wizard out. After getting knocked out the wizard wont have the focus required to ignore gravity and will start falling down. Upon hitting the ground they will have to wait for 20seconds before rejoining the game from their teams starting position.




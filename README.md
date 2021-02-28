# Quidittich Simulator
This project simulates a simple version of the quidditch game that features in the Harry Potter series.
The game's rules are simple. There are 2 teams ("griffindor and "slythrin") and one snitch inside the stadium. The 2 teams start at
the opposite ends of the stadium and once the game starts, all players will attempt to capture the snitch. Each catch of the snitch 
awards the catching player's team a point, succesive catches award 2 points. The first team to 100 points win.<br />

# Demo
Demo for game start with Team Size = 1 <br />
![Demo2](https://user-images.githubusercontent.com/69083495/109405366-981ce900-7978-11eb-8f1e-ba3b4e320ea9.gif)
<br />
Demo for game start with Team Size =  20 <br />
![Demo](https://user-images.githubusercontent.com/69083495/109405368-9eab6080-7978-11eb-888f-1eb138f85939.gif)

# Installing and Running Project
you need to install Unity 2019 before you can start using this project.<br />
You can get the project in one of 2 ways: <br />
<br />
     1- Use git to clone the repository<br />
          
      
          git clone https://github.com/KareemAbdelaty/Quidittich.git
       
   2- You can download the simulation package and directly import into unity
       

# Unity Interface and Controls guide
After importing the project into unity click on the play button to start the simulation. <br /><br />
while the simulation is running you can move the camera by using the keyboard<br />
## Camera controls
  **DownArrowKey**: move the camera backwards.<br />
  
  **UpArrowKey**: move the camera forwars.<br />
  
  **LeftArrowKey**: move the camera to the left<br />
  
  **RightArrowKey**: move the camera to the right <br />
  
  **Space**:move the camera upwards(increase height) <br />
  
  **N**: move the camera downwards(decrease height) <br />
  
  **Note:**<br />
        To rotate camera you need to use the unity inspector interface. Simply select the main camera in Scene and then change the rotation in the Inspector window
## Changing simulation configurations in the Inspector Window
Most of the simulation run configurations can be chaned in setup object using the Inspector window<br />
![Controls](https://user-images.githubusercontent.com/69083495/109405901-75410380-797d-11eb-9c69-3e80e8238ed4.png)![image](https://user-images.githubusercontent.com/69083495/109405970-02845800-797e-11eb-9676-d7efe4a35429.png)
<br />
Live game commentary will be found in the Console Tab<br />
![Uploading image.png…]()

## InSimulation UI
**Simulation running screen**<br />
![ingame](https://user-images.githubusercontent.com/69083495/109406386-36ad4800-7981-11eb-86d8-1f259e65db81.png)
<br />
**EndGame screen** <br />
![Endgame](https://user-images.githubusercontent.com/69083495/109406395-4b89db80-7981-11eb-9cd2-380916c0f6e6.png)



  

# Game narrative and Entities
<br />
There are 2 types of Entities inside the game <br />
**Snitch**: <br />
<br />

![snitch](https://user-images.githubusercontent.com/69083495/109402786-56347880-7961-11eb-9bb2-aeccd3e8ed98.gif)

<br />


**Snitch Behaviour** <br />
During the game, the snitch will choose a random point inside the stadium and start travelling towards it. If it reaches its destination the snitch will choose another random point to move towards. As a magical being the snitch is able to fly and ignore gravity. Finally, if a wizard gets too close to the snitch and catches it, the snitch will award the wizard's team a point and then escapes by teleporting itself away.
<br />

**Wizard**
<br />
![image](https://user-images.githubusercontent.com/69083495/109402033-297d6280-795b-11eb-9987-f71740bb8479.png)
![image](https://user-images.githubusercontent.com/69083495/109402009-0488ef80-795b-11eb-8462-de1a3b27881b.png) <br />
![ezgif com-gif-maker](https://user-images.githubusercontent.com/69083495/109402737-0655b180-7961-11eb-85b0-d386b1990653.gif)

Not all wizards are created equal. Wizards have a wide range of traits that affect how they preform in game. Here is a list of these traits:<br />
<br />
**Flying Ability**: some wizards are more confident in their flying abilities than their peers. This allows them to reach a higher maximum velocity.<br />

**Mind Control and Resistance**: some wizards are skilled in mind magic. Them allows them to mind control enemies that collide with them or to resist getting their mind controlled. Mind Controlled wizards change their outfit to that of the opposing team and any snitches they catch will count for the opposing team .wizards can shake off mind control after a while.<br />

**Invlunerablility** : Some wizards are able to avoid getting knocked out by making themselves invulnerable. This is hard to do so they are not always successful<br />

**Powershare**: If the wizard is knocked out there is a chance that the wizard will share his power with the rest of his team. Team mates return to normal when the wizard respawns<br />

**Fitness**: Flying is hard,as such wizards can get tired after flying for a long time. If a wizard is tired they will levitate in place to rest<br />
<br />
**Wizard Behaviour**<br />
During the game each wizard will look for the snitch and then start flying after it in order catch it while also trying to avoid colliding with the other wizards or the ground. . Although wizards try their best to not collide with each other or with the ground, sometimes collsions are inevitable. If a wizard collides with the ground they are knocked out for 20 seconds. When they wake up they have to make their way to their team's starting postion before they can rejoin the game. If a wizard collides with a wizard from another team the more aggressive wizard will knock the other wizard out. After getting knocked out the wizard wont have the focus required to ignore gravity and will start falling down. Upon hitting the ground they will have to wait for 20seconds before rejoining the game from their teams starting position.
<br />





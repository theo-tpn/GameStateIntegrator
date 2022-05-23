# GameStateIntegrator

## Overview 
GSIntegrator is a lightweight library that give access to listen POST data sent by CSGO.

GSIntegrator expose an observable property on which you can filter the data you want to handle.

You can find an Subscribe example in the GameStateIntegrator.App.Program.cs

## GameStateIntegration configuration file
On the game client side, create a config file that should look like :
```
"Console Sample v.1"
{
 "uri" "http://localhost:3000"
 "timeout" "5.0"
 "buffer"  "0.1"
 "throttle" "0.5"
 "heartbeat" "60.0"
 "auth"
 {
   "token" "TOKEN_HERE"
 }
 "output"
 {
   "precision_time" "3"
   "precision_position" "1"
   "precision_vector" "3"
 }
 "data"
 {
   "provider"            "1"
   "map"                 "1"
   "round"               "1"
   "player_id"           "1"
   "player_state"        "1"
   "player_weapons"      "1"
   "player_match_stats"  "1"
 }
}
```

/u/Bkid wrote an excellent post on reddit to go GSI in-depth : https://www.reddit.com/r/GlobalOffensive/comments/cjhcpy/game_state_integration_a_very_large_and_indepth/

You can also check the official doc by Vavle : https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration


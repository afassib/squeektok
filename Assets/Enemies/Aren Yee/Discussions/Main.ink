VAR has_met_aren = false
EXTERNAL playEmote(emoteName)
-> start
=== start ===
{has_met_aren == false:
    ~ has_met_aren = true
    -> meet_aren
- else:
    Oh. You again. Need directions *again*?
    * "Yes, please." -> lazy_directions
    * "No, just saying hi." -> just_hi
}
=== meet_aren ===
~ playEmote("Idle")
Huh?<br>Oh! Someone's<b></b> here...<br>Finally. I’ve been waiting...<b></b> like...<br>a long<b></b> long time. #speaker:????? #portrait:Aren_Yee_Idle #audio:animal_crossing_mid 
You were supposed to be here last month, I think? <br>Or maybe last year? #speaker:????? #portrait:Aren_Yee_Idle #audio:animal_crossing_mid
Doesn’t matter. #speaker:?????  #portrait:Aren_Yee_Idle #audio:animal_crossing_mid
~ playEmote("Clin_oeil")
So, uh... I’m Aren Yee. <br>I’m supposed to give you the tutorial or whatever. #speaker:"Aren Yee"  #portrait:Aren_Yee_Clin_oeil  #audio:celeste_low
~ playEmote("Angry")
But to be honest I don't feel like it.. #speaker:"Aren Yee" #portrait:Aren_Yee_Angry #layout:right  #audio:celeste_low
+ [tutorial?]
    Give me the tuto! #speaker:"You" #portrait:Player_Idle #layout:left
    As I said I’m not really *feeling* that today.<br>But hey — I'll point you in the right direction.<br>That’s just as good, right?<br>I need a coffee #speaker:"Aren Yee" #portrait:Aren_Yee_Angry #layout:right
    -> lazy_directions

+ Sorry I’m late?
    Sorry I'm late I guess ?    #speaker:"You" #portrait:Player_Idle #layout:left
    Yeah, well, I’ve had time to nap. So I forgive you. Sort of.
    Anyway, I’m Aren Yee, designated tutorial giver.
    But honestly? I’m not up for all that explaining.
    -> lazy_directions

+ "Where am I?"  #speaker:"You" #portrait:Player_Idle #layout:left
    I'm the First NPC. Big honor.
    but I dont have the right to rush the story.
    Can't tell you where you are, no!
    I was trained to guide new players... or just you but like... even this I’d rather nap.
    -> lazy_directions

=== lazy_directions ===
Aren Yee: So here’s the deal. You go up that hill — see the tree that looks like it’s flipping you off? Go past that.
Aren Yee: Then, left at the rock shaped like a grumpy turnip. Don’t ask.
Aren Yee: After that, something *should* happen. Probably a cutscene. Or monsters. Or both. I dunno.
Aren Yee: Good luck!

* "That’s it?"
    Aren Yee: What? You want a manual? I gave you *directions*. Classic NPC stuff. You're welcome.
    ->END
* "Thanks, I guess."
    Aren Yee: No prob. Come back if you get lost. Or don’t. I’ll be here either way.
    ->END
* "You’re really not into this, huh?"
    Aren Yee: Eh. I’m more of a chill NPC. I believe in *player discovery*. Also naps.
    ->END
-> END

=== just_hi ===
Oh. Cool. Well, uh... hi back.
Still not doing the tutorial though.
Smash all the buttons again you will get it...
-> END

using System.Collections;
using System.Collections.Generic;
using Unigine;
using UnigineApp.data.Events;

[Component(PropertyGuid = "fb4975dacacdf3ebfd94a3d72f9867f89cd77b08")]
public class SimulateEvent: Component
{
	private AchievementEventHandler ASys = new();
	private DamageEventHandler DSys = new();

	void Init()
	{
		ASys.Init();
		DSys.Init();
	}
	
	void Update()
	{
		if (Input.IsKeyDown(Input.KEY.U)) { 
			var eManager = EventManager.Instance;
			eManager.Achievement.RunEvent(ACHIEVEMENTS_EVENT.EVENT_CLEARED);
		}

        if (Input.IsKeyDown(Input.KEY.I))
        {
            var eManager = EventManager.Instance;
            eManager.Achievement.RunEvent(ACHIEVEMENTS_EVENT.EVENT_CLEARED);
        }

        if (Input.IsKeyDown(Input.KEY.O))
        {
            var eManager = EventManager.Instance;
            eManager.State.RunEvent(PLAYER_STATE_EVENT.POISONED);
        }
    }
}
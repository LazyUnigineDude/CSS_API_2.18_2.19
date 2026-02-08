using Unigine;

namespace UnigineApp.data.Events
{
    internal class AchievementEventHandler
    {
        public void Init()
        {
            var em = EventManager.Instance;

            em.Achievement.OnEvent += OnAchievement;
            em.Achievement.OnEvent += OnHiddenAchievement;
        }

        private void OnAchievement(ACHIEVEMENTS_EVENT e)
        {
            if (e == ACHIEVEMENTS_EVENT.EVENT_CLEARED)
            {
                Log.Message("Achievement cleared!\n");
                EventManager.Instance.Achievement.OnEvent -= OnAchievement;
            }
        }

        private void OnHiddenAchievement(ACHIEVEMENTS_EVENT e)
        {
            if (e == ACHIEVEMENTS_EVENT.HIDDEN_EVENT_CLEARED)
            {
                Log.Message("Hidden cleared!\n");
                EventManager.Instance.Achievement.OnEvent -= OnHiddenAchievement;
            }
        }
    }

    internal class DamageEventHandler
    {
        private int PlayerHealth = 10;

        public void Init()
        {
            EventManager.Instance.State.OnEvent += OnStateChanged;
        }

        private void OnStateChanged(PLAYER_STATE_EVENT e)
        {
            if (e == PLAYER_STATE_EVENT.POISONED)
            {
                HealthDmg(2);
                Log.Message($"You've Been Poisoned! Health now {PlayerHealth}\n");
            }

            if (e == PLAYER_STATE_EVENT.DEAD)
            {
                Log.Message("You Dead!\n");
                EventManager.Instance.State.OnEvent -= OnStateChanged;
            }
        }

        private void HealthDmg(int val)
        {
            var em = EventManager.Instance;
            PlayerHealth -= val;

            em.Damage.RunEvent(new DAMAGE_EVENT(val));

            if (PlayerHealth <= 0)
                em.State.RunEvent(PLAYER_STATE_EVENT.DEAD);
        }
    }
}

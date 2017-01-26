using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Warwirk
{
    class Program
    {
        public static Menu Menu,
            DrawMenu,
            ComboMenu;
            


        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Skillshot R;


        public static EloBuddy.AIHeroClient _Player
        {
            get { return EloBuddy.ObjectManager.Player; }
        }

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Drawing.OnDraw += Game_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnStart(EventArgs args)
        {
            Chat.Print("Warwick BETA 1v");

            Q = new Spell.Skillshot(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R);

            Menu = MainMenu.AddMenu("Warwirk", "Warwirk");
            Menu.AddSeparator();
            Menu.AddLabel("By:Enemy ");

            DrawMenu = Menu.AddSubMenu("Draw", "Draw");
            DrawMenu.Add("drawDisable", new CheckBox("Disable all Draws", true));
            ComboMenu = Menu.AddSubMenu("Combo", "ComboWW");
            ComboMenu.Add("Combo Q", new CheckBox("Use Q in combo", true));
            ComboMenu.Add("Combo W", new CheckBox("Use W in combo", true));
            ComboMenu.Add("Combo E", new CheckBox("Use E in combo", true));
            ComboMenu.Add("Combo R", new CheckBox("Use R in combo", true));

        
        }

        public static void Game_OnDraw(EventArgs args)
        {
            if (!DrawMenu["Disable"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.Blue, Radius = ObjectManager.Player.GetAutoAttackRange(), BorderWidth = 2f }.Draw(ObjectManager.Player.Position);
            } 
            
        }
        public static void Game_OnUpdate(EventArgs args)
        {

            var alvo = TargetSelector.GetTarget(1000, DamageType.Physical);

            if(!alvo.IsValid()) return;

            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                if (Q.IsReady() && ComboMenu["Combo Q"].Cast<CheckBox>().CurrentValue)
                {
                    Q.Cast();
                }

                if (W.IsReady() && _Player.Distance(alvo) <= W.Range + _Player.GetAutoAttackRange() && ComboMenu["Combo W"].Cast<CheckBox>().CurrentValue)
                {
                    W.Cast(alvo);
                }

                if (E.IsReady() && E.IsInRange(alvo) && ComboMenu["Combo E"].Cast<CheckBox>().CurrentValue)
                {
                    E.Cast(alvo);
                }

                if (R.IsReady() && R.IsInRange(alvo) && ComboMenu["Combo R"].Cast<CheckBox>().CurrentValue)
                {
                    R.Cast(alvo);
                }
            }

        }
    }
}


      

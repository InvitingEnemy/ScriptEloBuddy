﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;

namespace kTristana
{
    class Program
    {
        public static Menu Menu,
            DrawMenu,
            ComboMenu;

        public static Spell.Active Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Targeted R;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Drawing.OnDraw += Game_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnStart(EventArgs args)
        {
            Chat.Print("kTristana Addon Beta");

            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular);
            E = new Spell.Targeted(SpellSlot.E, 575);
            R = new Spell.Targeted(SpellSlot.R, 575);

            Menu = MainMenu.AddMenu("kTristana", "kTristana");
            Menu.AddSeparator();
            Menu.AddLabel("Enemy");

            DrawMenu = Menu.AddSubMenu("Draw", "DrawTristana");
            DrawMenu.Add("drawDisable", new CheckBox("Desabilitar todos os Draws", true));
            ComboMenu = Menu.AddSubMenu("Combo", "macComboTristana");
            ComboMenu.Add("comboQ", new CheckBox("Use Q  combo", true));
            ComboMenu.Add("comboW", new CheckBox("Use W  combo", true));
            ComboMenu.Add("comboE", new CheckBox("Use E  combo", true));
            ComboMenu.Add("comboR", new CheckBox("Use R  combo", true));

            { }

            throw new NotImplementedException();
        }

        private static void Game_OnDraw(EventArgs args)
        {
            if (!DrawMenu["drawDisable"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.Blue, Radius = ObjectManager.Player.GetAutoAttackRange(), BorderWidth = 2f }.Draw(ObjectManager.Player.Position);
            }
            { }
            throw new NotImplementedException();
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var alvo = TargetSelector.GetTarget(1000, DamageType.Physical);

            if(!alvo.IsValid()) return;

            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                if (Q.IsReady() && ComboMenu["comboQ"].Cast<CheckBox>().CurrentValue)
                {
                    Q.Cast();
                }

                if (W.IsReady() && _Player.Distance(alvo) <= W.Range + _Player.GetAutoAttackRange() && ComboMenu["comboW"].Cast<CheckBox>().CurrentValue)
                {
                    W.Cast(alvo);
                }

                if (E.IsReady() && E.IsInRange(alvo) && ComboMenu["comboE"].Cast<CheckBox>().CurrentValue)
                {
                    E.Cast(alvo);
                }

                if (R.IsReady() && R.IsInRange(alvo) && ComboMenu["comboR"].Cast<CheckBox>().CurrentValue)
                {
                    R.Cast(alvo);
                }
            }
    
        throw new NotImplementedException();
        }
    }
}

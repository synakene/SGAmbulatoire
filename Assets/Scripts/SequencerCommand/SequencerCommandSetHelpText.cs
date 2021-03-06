﻿using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {


    public class SequencerCommandSetHelpText : SequencerCommand
    {
        public void Start()
        {
            string var = GetParameter(0);
            Transform helpTransform = GameObject.Find("HelpCanvas").transform;
            Text[] helpTextTab = helpTransform.gameObject.GetComponentsInChildren<Text>(true);

            Text helpText = helpTextTab[0];
            Text titre = helpTextTab[0];
            foreach (Text t in helpTextTab)
            {
                if (t.name == "Text")
                    helpText = t;
                if (t.name == "Titre")
                    titre = t;
            }

            switch (var)
            {
                case "choix1":
                    helpText.text = "Attention à votre ergonomie, rester debout pendant tout l’entretien va vous fatiguer. De plus, vous n’êtes pas à la même hauteur que le patient pour communiquer.";
                    titre.text = "Aide";
                    break;
                case "choix2":
                    helpText.text = "Ce sont les affaires du patient !";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "choix3":
                    helpText.text = "Attention à l’hygiène, ce n’est pas votre lit ! De plus, le patient peut ressentir votre geste comme une intrusion dans son espace personnel.";
                    titre.text = "Aide";
                    break;
                case "param1":
                    helpText.text = "Tension artérielle : 150/100mmHg";
                    titre.text = "Résultat";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "param2":
                    helpText.text = "Température : 36,2°C";
                    titre.text = "Résultat";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "param3":
                    helpText.text = "Saturation : 99 %";
                    titre.text = "Résultat";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "param4":
                    helpText.text = "Fréquence cardiaque : 95 batt/min";
                    titre.text = "Résultat";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diag":
                    helpText.text = "Revoyez les normes des paramètres vitaux !";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "reponseDiag":
                    helpText.text = "Le patient souffre d'une hypertension et d'une tachycardie";
                    titre.text = "Echec";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diagConsigne":
                    helpText.text = "Vous allez devoir élaborer un diagnostic. Posez les questions les plus pertinentes au patient avant de poser un diagnostic. Vous pouvez consulter les questions et réponses déjà posées au patient, en cliquant sur l'icone du carnet en haut à gauche. Quand vous avez terminé cliquez sur \"Poser un diagnostic\". Vous pourrez toujours revenir aux questions avant de poser un diagnostic si vous le désirez.";
                    titre.text = "Consigne";
                    helpText.alignment = TextAnchor.MiddleLeft;
                    break;
                case "gratz1":
                    helpText.text = "Vous avez posé le bon diagnostic";
                    titre.text = "Bravo !";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diag1":
                    helpText.text = "Constipation avérée = 72h sans selles";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diag2":
                    helpText.text = "Bonne idée car le patient a des sueurs, est à jeun mais glycémie correcte et absence de vertiges.";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diag3":
                    helpText.text = "36°C < température normale < 38°C";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "diag0":
                    helpText.text = "Bravo ! Vous avez bien repéré les signes de l’anxiété chez Mr Sainte-Marie, tremblements, se ronge les ongles, sueurs, douleurs abdominales, troubles du sommeil, tachycardie et hypertension.";
                    titre.text = "Bon diagnostic !";
                    helpText.alignment = TextAnchor.MiddleLeft;
                    break;
                case "infoGlycemie":
                    helpText.text = "Résultat : 4.1 mmol/L";
                    titre.text = "Information";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "echecDiag1":
                    helpText.text = "Attention, vous n'avez pas réaliser l'ensemble des examens necessaires";
                    titre.text = "Aide";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "echecDiag2":
                    helpText.text = "Dans ce cas il est necessaire d'effectuer les examens suivant :\n - Prise de la tension artérielle \n - Prise de la température \n - Prise de la saturation \n - Prise de la fréquence cardiaque";
                    titre.text = "Examen incomplet";
                    helpText.alignment = TextAnchor.MiddleLeft;
                    break;
                case "identite":
                    helpText.text = "Vous ne pouvez pas continuer votre entretien avec votre patient si vous n’avez pas toutes les informations le concernant dans un premier temps.";
                    titre.text = "Echec";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "echecDouleur":
                    helpText.text = "Ce n’est pas le moment d’appeler le médecin, vous devez pouvoir résoudre ce genre de situation par vous-même";
                    titre.text = "Echec";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "infectionBijou":
                    helpText.text = "Pour prévenir le risque infectieux, les bijoux ne sont pas autorisés au bloc.";
                    titre.text = "Echec";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "infectionDouche":
                    helpText.text = "Risque infectieux";
                    titre.text = "Echec";
                    helpText.alignment = TextAnchor.MiddleCenter;
                    break;
                case "priseNote":
                    helpText.text = "Afin de poser un diagnostic dans les meilleures conditions, une prise de note a été effectuée, visible en cliquant sur le bouton du carnet situé en haut à gauche.";
                    titre.text = "Information";
                    helpText.alignment = TextAnchor.MiddleLeft;
                    break;
				case "ord1":
					helpText.text = "Vous avez effectué les vérifications d'hygiène dans le bon ordre.";
					titre.text = "Bonne réponse";
					helpText.alignment = TextAnchor.MiddleCenter;
					break;
				case "ord2":
					helpText.text = "La dépilation chimique doit être faite avant la douche. De plus, il est important qu’aucun bijou ne soit présent au moment de la douche.";
					titre.text = "Mauvaise réponse";
					helpText.alignment = TextAnchor.MiddleCenter;
					break;
				case "ord3":
					helpText.text = "La dépilation chimique doit être faite avant la douche.";
					titre.text = "Mauvaise réponse";
					helpText.alignment = TextAnchor.MiddleCenter;
					break;
            }
            Stop();
        }
    }
}

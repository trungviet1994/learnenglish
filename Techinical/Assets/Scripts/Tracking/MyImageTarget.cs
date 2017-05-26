using UnityEngine;
using System.Collections;
using Vuforia;
using DG.Tweening;

public class MyImageTarget : DefaultTrackableEventHandler
{
    public string letter = "";
    public eBaseTeamType teamMode;
    //private GameObject m_objectAnimal;
    //private float m_scaleStartValue;
    //private AnimalObject animalScripts;
    //void Awake()
    //{
    //    if (teamMode == eBaseTeamType.TEAM_RED)
    //    {
    //        m_objectAnimal = transform.GetChild(0).gameObject;
    //        if (m_objectAnimal)
    //        {
    //            m_scaleStartValue = m_objectAnimal.transform.localScale.x;
    //            m_objectAnimal.transform.localScale = Vector3.zero;
    //            m_objectAnimal.SetActive(false);
    //            //animalScripts = m_objectAnimal.GetComponentInChildren<AnimalObject>();
    //        }
    //    }
    //}
    protected override void OnTrackingFound()
    {
        if(GamePlayConfig.Instance.GameStart)
        {
            if (GamePlayConfig.Instance.UserPlayMode == eUserPlayMode.MULTI_PLAY)
            {
                GameLogic.Instance.CheckLetterTrackable(this.letter,this.teamMode);
            }
            else
            {
                GameLogic.Instance.CheckLetterTrackable(this.letter, eBaseTeamType.NONE);
            }
        }
        //else
        //{
        //    if(GamePlayConfig.Instance.m_modePlay == ModePlay.PLAY_ANIMAL)
        //    {
        //        if (m_objectAnimal)
        //        {
        //            m_objectAnimal.transform.localScale = Vector3.zero;
        //            m_objectAnimal.transform.DOScale(Vector3.one * m_scaleStartValue, 0.75f);
        //            ManagerObject.Instance.SpawnObjectByType(eObjectType.par_animalAppear, ePoolName.ObjectPool).transform.position = transform.position;
        //            m_objectAnimal.SetActive(true);
        //            GameController.Instance.m_listAnimalTracked.Add(m_objectAnimal);
        //            AudioManager.Instance.PlayAudioByTypeName(eAudioName.APPEAR);
        //        }
        //    }
        //}
    }

    protected override void OnTrackingLost()
    {
        //if (m_objectAnimal && m_objectAnimal.activeInHierarchy)
        //{
        //    GameController.Instance.RemoveAnimal(m_objectAnimal);
        //    m_objectAnimal.SetActive(false);
        //}
    }
}

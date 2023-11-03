using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ArcherAnimation : MonoBehaviour
{
    [SerializeField] SkeletonAnimation archerSkelet;
    void Start()
    {
        archerSkelet.AnimationState.SetAnimation(0, "idle", true);
    }

  public void AttackStart(int trackIndex) => archerSkelet.AnimationState.SetAnimation(trackIndex, "attack_start", false);
  public void AttackTarget(int trackIndex) => archerSkelet.AnimationState.SetAnimation(trackIndex, "attack_target", true);
  public void AttackFinish(int trackIndex) => archerSkelet.AnimationState.SetAnimation(trackIndex, "attack_finish", false);
  public void Idle(int trackIndex) => archerSkelet.AnimationState.SetAnimation(trackIndex, "idle", true);

}

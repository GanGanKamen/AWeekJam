using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRIByte : MonoBehaviour
{
    [Header("ファイルパス")] public string path;
    [Header("ACFファイル")] public string acf;
    [Header("ACBファイル")] public string[] acb;
    [Header("Dpsバス設定")] public string DspBusSetting;
    byte[] raw_dataAcf;
    List<byte[]> raw_dataAcb;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        raw_dataAcb = new List<byte[]>();
        // テキストアセットとしてACFを読み込む。NewProject.bytesはResourcesフォルダに配置する。
        TextAsset text_assetAcf = Resources.Load<TextAsset>(path + acf);
        raw_dataAcf = text_assetAcf.bytes;

        // テキストアセットとしてACBを読み込む。CueSheet_0.bytesはResourcesフォルダに配置する。
        // ----
        for (int i = 0; i < acb.Length; i++)
        {
            TextAsset text_assetAcb = Resources.Load<TextAsset>(path + acb[i]);
            raw_dataAcb.Add(text_assetAcb.bytes);
        }
        // ACF登録
        CriAtomEx.RegisterAcf(raw_dataAcf);
        // CriAtomを作成しゲームオブジェクトに追加
        // ----
        this.gameObject.AddComponent<CriAtom>();
        // ----
        // キューシートを追加
        var cuesheet = new CriAtomCueSheet[acb.Length];
        for (int i = 0; i < acb.Length; i++)
        {
             cuesheet[i]= CriAtom.AddCueSheet(acb[i], raw_dataAcb[i], null);
        }
        
        // ----
        // キューシートのロード完了まで待つ WebGL版はロードに時間がかかる（ロード前にAtomにアクセスしないようにここで待つ）
        // ----
        while (cuesheet[acb.Length-1].IsLoading)
        {
            yield return new WaitForEndOfFrame();
        }
        
        CriAtom.AttachDspBusSetting(DspBusSetting);

    }

    static public void PlaySound(GameObject soundobj,string cueSheet,string cueName)
    {
        CriAtomSource atomSource;
        if(soundobj.GetComponent<CriAtomSource>() == null)
        {
            soundobj.AddComponent<CriAtomSource>();
        }
        atomSource = soundobj.GetComponent<CriAtomSource>();
        atomSource.cueSheet = cueSheet;
        atomSource.Play(cueName);
    }

    static public void PlaySound(GameObject soundobj, string cueSheet, int id)
    {
        CriAtomSource atomSource;
        if (soundobj.GetComponent<CriAtomSource>() == null)
        {
            soundobj.AddComponent<CriAtomSource>();
        }
        atomSource = soundobj.GetComponent<CriAtomSource>();
        atomSource.cueSheet = cueSheet;
        atomSource.Play(id);
    }

}
using System;

namespace Day9;

public class Sequence
{
    private int[] _sequence;
    private Sequence? _parent = null;

    public int this[int index] { get => _sequence[index]; }
    public int Length { get => _sequence.Length; }
    public int First { get => _sequence[0]; }
    public int Last { get => _sequence[_sequence.Length-1]; }
    public Sequence Parent { get => _parent!; }

    public Sequence(string seq_txt){
        string[] seq = seq_txt.Split(' ');

        _sequence = new int[seq.Length];

        for(int i=0; i<seq.Length; i++){
            _sequence[i] = Int32.Parse(seq[i]);
        }
    }

    public Sequence(int[] seq, Sequence? parent = null){
        _sequence = new int[seq.Length];
        for(int i=0; i<seq.Length; i++){
            _sequence[i] = seq[i];
        }

        _parent = parent;
    }

    public void Add(int value){
        int[] temp = new int[_sequence.Length + 1];
        for(int i=0; i<temp.Length-1; i++){
            temp[i] = _sequence[i];
        }
        temp[_sequence.Length] = value;
        _sequence = temp;
    }

    public void AddFirst(int value){
        int[] temp = new int[_sequence.Length + 1];
        for(int i=0; i<temp.Length-1; i++){
            temp[i+1] = _sequence[i];
        }
        temp[0] = value;
        _sequence = temp;
    }

    public Sequence GetDifferences(){
        int[] diffs = new int[_sequence.Length-1];

        for(int i=0; i<diffs.Length; i++){
            diffs[i] = _sequence[i+1]-_sequence[i];
        }

        return new Sequence(diffs, this);
    }

    public Boolean IsAllZero(){
        for(int i=0; i<_sequence.Length; i++){
            if(_sequence[i] != 0){
                return false;
            }
        }
        return true;
    }

    public override string ToString(){
        string str = "";

        for(int i=0; i<_sequence.Length; i++){
            str += _sequence[i]+" ";
        }

        return str.Substring(0,str.Length-1);
    }
}

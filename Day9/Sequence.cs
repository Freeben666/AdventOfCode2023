using System;

namespace Day9;

public class Sequence
{
    private int[] _sequence;
    private Sequence? _parent = null;

    public Sequence Parent { get => _parent!; }

    public Sequence(string seq_txt){
        string[] seq = seq_txt.Split(' ');

        _sequence = new int[seq.Length];

        for(int i=0; i<seq.Length; i++){
            _sequence[i] = Int32.Parse(seq[i]);
        }
    }

    public Sequence(int[] seq, Sequence? parent = null){
        _sequence = seq.Clone();
        _parent = parent;
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
}

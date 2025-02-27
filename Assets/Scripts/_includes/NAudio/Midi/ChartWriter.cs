﻿using System;
using System.Collections.Generic;
using System.IO;

namespace mid2chart {
    internal static class ChartWriter {
        private static int hopoThresh = 64;
        internal static void WriteChart(Song s, string path, bool dummy) {
            using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) {
                using (var file = new StreamWriter(output)) {
                    file.WriteLine("[Song]");
                    file.WriteLine("{");
                    file.WriteLine("\tName = \"" + s.songname + "\"");
                    if (s.artist != "") file.WriteLine("\tArtist = \"" + s.artist + "\"");
                    if (s.year != "") file.WriteLine("\tYear = \", " + s.year + "\"");
                    if (s.charter != "") file.WriteLine("\tCharter = \"" + s.charter + "\"");
                    file.WriteLine("\tOffset = " + GetOffsetString(s.offset));
                    file.WriteLine("\tResolution = 192");
                    file.WriteLine("\tPlayer2 = bass");
                    file.WriteLine("\tDifficulty = 0");
                    file.WriteLine("\tPreviewStart = 0.00");
                    file.WriteLine("\tPreviewEnd = 0.00");
                    file.WriteLine("\tGenre = \"rock\"");
                    file.WriteLine("\tMediaType = \"cd\"");
                    file.WriteLine("\tMusicStream = \"song.ogg\"");
                    file.WriteLine("\tGuitarStream = \"guitar.ogg\"");
                    file.WriteLine("\tBassStream = \"rhythm.ogg\"");
                    file.WriteLine("}");
                    file.WriteLine("[SyncTrack]");
                    file.WriteLine("{");
                    foreach (Sync sync in s.sync) {
                        if (!sync.isBPM)
                            file.WriteLine("\t" + sync.tick + " = TS " + sync.num);
                        else
                            file.WriteLine("\t" + sync.tick + " = B " + sync.num);
                    }
                    file.WriteLine("}");
                    file.WriteLine("[Events]");
                    file.WriteLine("{");
                    foreach (Section ss in s.sections) {
                        file.WriteLine("\t" + ss.tick + " = E \"section " + ss.name + "\"");
                    }
                    file.WriteLine("}");
                    if (s.eGuitar.Count > 0) {
                        file.WriteLine("[EasySingle]");
                        file.WriteLine("{");
                        WriteNotes(file, s.eGuitar, s.eGuitarForceHOPO, s.eGuitarForceStrum, s.tapGuitar, s.eGuitarOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.eBass.Count > 0) {
                        file.WriteLine("[EasyDoubleBass]");
                        file.WriteLine("{");
                        WriteNotes(file, s.eBass, s.eBassForceHOPO, s.eBassForceStrum, s.tapBass, s.eBassOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.eKeys.Count > 0) {
                        file.WriteLine("[EasyEnhancedGuitar]");
                        file.WriteLine("{");
                        WriteNotes(file, s.eKeys, new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), true);
                        file.WriteLine("}");
                    }
                    if (s.mGuitar.Count > 0) {
                        file.WriteLine("[MediumSingle]");
                        file.WriteLine("{");
                        WriteNotes(file, s.mGuitar, s.mGuitarForceHOPO, s.mGuitarForceStrum, s.tapGuitar, s.mGuitarOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.mBass.Count > 0) {
                        file.WriteLine("[MediumDoubleBass]");
                        file.WriteLine("{");
                        WriteNotes(file, s.mBass, s.mBassForceHOPO, s.mBassForceStrum, s.tapBass, s.mBassOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.mKeys.Count > 0) {
                        file.WriteLine("[MediumEnhancedGuitar]");
                        file.WriteLine("{");
                        WriteNotes(file, s.mKeys, new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), true);
                        file.WriteLine("}");
                    }
                    if (s.hGuitar.Count > 0) {
                        file.WriteLine("[HardSingle]");
                        file.WriteLine("{");
                        WriteNotes(file, s.hGuitar, s.hGuitarForceHOPO, s.hGuitarForceStrum, s.tapGuitar, s.hGuitarOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.hBass.Count > 0) {
                        file.WriteLine("[HardDoubleBass]");
                        file.WriteLine("{");
                        WriteNotes(file, s.hBass, s.hBassForceHOPO, s.hBassForceStrum, s.tapBass, s.hBassOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.hKeys.Count > 0) {
                        file.WriteLine("[HardEnhancedGuitar]");
                        file.WriteLine("{");
                        WriteNotes(file, s.hKeys, new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), true);
                        file.WriteLine("}");
                    }
                    if (s.xGuitar.Count > 0) {
                        file.WriteLine("[ExpertSingle]");
                        file.WriteLine("{");
                        WriteNotes(file, s.xGuitar, s.xGuitarForceHOPO, s.xGuitarForceStrum, s.tapGuitar, s.xGuitarOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.xBass.Count > 0) {
                        file.WriteLine("[ExpertDoubleBass]");
                        file.WriteLine("{");
                        WriteNotes(file, s.xBass, s.xBassForceHOPO, s.xBassForceStrum, s.tapBass, s.xBassOpenNotes, false);
                        file.WriteLine("}");
                    }
                    if (s.xKeys.Count > 0) {
                        file.WriteLine("[ExpertEnhancedGuitar]");
                        file.WriteLine("{");
                        WriteNotes(file, s.xKeys, new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), new List<NoteSection>(), true);
                        file.WriteLine("}");
                    }
                }
            }
        }

        private static string GetOffsetString(long offset) {
            var str = "";
            if (offset < 0) {
                str = "-";
                offset = offset * -1;
            }
            if (offset == 0) str += "0";
            else if (offset < 10) str += "0.00" + offset;
            else if (offset < 100) str += "0.0" + offset;
            else if (offset < 1000) str += "0." + offset;
            else str += offset.ToString().Substring(0, 1) + "." + offset.ToString().Substring(1);
            return str;
        }

        private static void WriteNotes(StreamWriter file, List<Event> notes, List<NoteSection> forceHOPO, List<NoteSection> forceStrum, List<NoteSection> tap, List<NoteSection> openNotes, bool isKeys) {
            foreach (Event e in notes) {
                var n = e as MIDNote;
                if (n != null) {
                    if (IsTap(n, openNotes)) {
                            file.WriteLine("\t" + n.tick + " = N 7 " + n.sus);
                    } else {
                        file.WriteLine("\t" + n.tick + " = N " + n.note + " " + n.sus);
                    }
                    if (GetNextNoteDiff(n, notes) > 0) {
                        if (IsTap(n, tap) && !IsTap(n, openNotes))
                                file.WriteLine("\t" + n.tick + " = N 6 0");
                        else if ((!isKeys && IsForced(n, notes, forceHOPO, forceStrum, openNotes)) || (isKeys && IsForcedKeys(n, notes)))
                                file.WriteLine("\t" + n.tick + " = N 5 0");
                    }
                    continue;
                }
                var sp = e as NoteSection;
                if (sp != null) {
                    file.WriteLine("\t" + sp.tick + " = S 2 " + sp.sus);
                }
            }
        }

        private static
            bool IsForced(MIDNote n, List<Event> notes, List<NoteSection> forceHOPO, List<NoteSection> forceStrum, List<NoteSection> openNotes) {
            bool check = false;
            long tickDiff = GetPreviousNoteDiff(n, notes);
            if (tickDiff <= hopoThresh && !IsChord(n, notes) && !SameNoteAsPrevious(n, notes, openNotes)) {
                if ((ContainsNoteFromPreviousChord(n, notes)) || (IsTap(n, openNotes))) {
                    //applies harmonix's post-chord ho/po logic
                    //this means that this note contains a note from the previous chord
                    //therefore it's a strum, unless it's forced otherwise
                    //so, look on forceHOPO
                    //also works when forcing open notes as strum by default
                    //unless forced otherwise ofc
                    check = true;
                    foreach (var hopo in forceHOPO) {
                        if (hopo.tick > n.tick) break;
                        if (n.tick >= hopo.tick && n.tick < (hopo.tick + hopo.sus)) {
                            check = false;
                            break;
                        }
                    }
                } else {
                    //regular HOPO - look on forceStrum
                    foreach (var s in forceStrum) {
                        if (s.tick > n.tick) break;
                        if (n.tick >= s.tick && n.tick < (s.tick + s.sus)) {
                            check = true; break;
                        }
                    }
                }
                if ((tickDiff > 64 && tickDiff <= 96)) check = !check;
            } else /*if (tickDiff > hopoThresh || IsChord(n, notes) || SameNoteAsPrevious(n, notes))*/
              { //not HOPO - look on forceHOPO
                foreach (var hopo in forceHOPO) {
                    if (hopo.tick > n.tick) break;
                    if (n.tick >= hopo.tick && n.tick < (hopo.tick + hopo.sus)) {
                        check = true;
                        if ((SameNoteAsPrevious(n, notes, openNotes) || SameChordAsPrevious(n, notes))) { check = false; break; }
                        if (IsChord(n, notes)) check = false; break;
                    }
                }
                if ((tickDiff > 32 && tickDiff <= 64) && !IsChord(n, notes) && !SameNoteAsPrevious(n, notes, openNotes)) check = !check;
            }
            return check;
        }

        private static bool IsForcedKeys(MIDNote n, List<Event> notes) {
            var tickDiff = GetPreviousNoteDiff(n, notes);
            if (tickDiff > hopoThresh)
                    return false;

            if (!IsChord(n, notes) && ContainsNoteFromPreviousChord(n, notes))
                    return true;

            if (!IsChord(n, notes))
                    return false;

            return !SameChordAsPrevious(n, notes);
        }

        private static bool SameChordAsPrevious(MIDNote n, List<Event> notes) {
            if (!IsChord(n, notes)) return false;
            var previousNote = new MIDNote(0, -1, 0);
            for (var i = notes.IndexOf(n) - 1; i >= 0; i--) {
                previousNote = notes[i] as MIDNote;
                if (previousNote != null && n.tick - previousNote.tick != 0) {
                    break;
                }
            }
            if (!IsChord(previousNote, notes)) return false;
            //if the current note isnt a chord it returns false
            //else it looks for the previous note - if it also isnt a chord, it returns false again
            var currentChord = GetNotesFromChord(n, notes);
            var previousChord = GetNotesFromChord(previousNote, notes);
            if (currentChord.Count != previousChord.Count) return false;
            //it gets the respective chords and compares their length - if they dont match they cannot be the same chord
            var count = currentChord.Count;

            //OLD CODE, LEAVING THIS INCASE NEW ONE FAILS
            /*bool[] check1 = new bool[count], check2 = new bool[count];
            for (var i = 0; i < count; i++) {
                for (var j = 0; j < count; j++) {
                    if (check2[j]) break;
                    if (currentChord[i].note == previousChord[j].note) {
                        check1[i] = true; check2[j] = true; break;
                    }
                }
                if (!check1[i]) return false;
            }
            //tries to find the note from the current chord in the previous chord.
            //stores whether it's been found or not in 2 arrays (one for each chord).
            //if the notes match somewhere both their positions are marked as true on their respective arrays
            for (var i = 0; i < count; i++)
                if (!check1[i] || !check2[i]) return false;
            //checks if ALL of the notes were found - if they werent then they arent the same chord
            //if it didnt fail until here then it means both chords are the same*/

            for (var i = 0; i < count; i++) {
                if (currentChord[i].note != previousChord[i].note) return false;
            }
            return true;
        }

        public static List<MIDNote> GetNotesFromChord(MIDNote n, List<Event> notes) {
            var chord = new List<MIDNote> { n };
            var index = notes.IndexOf(n);
            for (var i = index + 1; i < notes.Count; i++) {
                var n2 = notes[i] as MIDNote;
                if (n2 != null && notes[i].tick - n.tick == 0) chord.Add(n2);
                if (n2 != null && notes[i].tick - n.tick != 0) break;
            }
            for (var i = index - 1; i >= 0; i--) {
                var n2 = notes[i] as MIDNote;
                if (n2 != null && notes[i].tick - n.tick == 0) chord.Add(n2);
                if (n2 != null && notes[i].tick - n.tick != 0) break;
            }
            return chord;
        }

        private static bool ContainsNoteFromPreviousChord(MIDNote n, List<Event> notes) {
           
            var previousChord = new List<MIDNote>();
            MIDNote previousNote = null;
            for (var i = notes.IndexOf(n) - 1; i >= 0; i--) {
                previousNote = notes[i] as MIDNote;
                if (previousNote != null)
                    break;
            }
            if (!IsChord(previousNote, notes)) return false;
            previousChord = GetNotesFromChord(previousNote, notes);
            /*while(true) {
                if (notes.IndexOf(previousNote) == 0)
                    break;
                if (previousNote != null && previousNote.tick-notes[(notes.IndexOf(previousNote)-1)].tick != 0)
                    break;
                previousNote = notes[(notes.IndexOf(previousNote)-1)] as Note;
                if (previousNote != null)
                    previousChord.Add(previousNote);
            }*/
            foreach (var n2 in previousChord) {
                if (n2.note == n.note) return true;
            }
            return false;
        }

        private static bool SameNoteAsPrevious(MIDNote n, List<Event> notes, List<NoteSection> openNotes) {
            if (notes.IndexOf(n) == 0) return false;
            for (int i = notes.IndexOf(n) - 1; i >= 0; i--) {
                var n2 = notes[i] as MIDNote;
                if (n2 != null) {
                    return !IsChord(n2, notes) && n2.note == n.note;
                }
            }
            return false;
        }

        private static bool IsChord(MIDNote n, List<Event> notes) {
            var index = notes.IndexOf(n);
            if (index == 0) {
                for (var i = 1; i < notes.Count; i++) {
                    var n2 = notes[i] as MIDNote;
                    if (n2 != null) return notes[i].tick - n.tick == 0;
                }
            }
            if (index == notes.Count - 1) {
                for (var i = notes.Count - 2; i >= 0; i--) {
                    var n2 = notes[i] as MIDNote;
                    if (n2 != null) return notes[i].tick - n.tick == 0;
                }
            }
            bool check = false;
            for (var i = index + 1; i < notes.Count; i++) {
                var n2 = notes[i] as MIDNote;
                if (n2 != null && notes[i].tick - n.tick == 0) check = true;
                if (n2 != null && notes[i].tick - n.tick != 0) break;
            }
            if (check) return check;
            for (var i = index - 1; i >= 0; i--) {
                var n2 = notes[i] as MIDNote;
                if (n2 != null && notes[i].tick - n.tick == 0) check = true;
                if (n2 != null && notes[i].tick - n.tick != 0) break;
            }
            return check;
        }

        private static long GetPreviousNoteDiff(MIDNote n, List<Event> notes) {
            if (notes[0] == n) return 192;
            else {
                for (int i = notes.IndexOf(n) - 1; i >= 0; i--) {
                    var previousNote = notes[i] as MIDNote;
                    if (previousNote != null && n.tick - previousNote.tick != 0) {
                        return n.tick - previousNote.tick;
                    }

                }
            }
            return 192;
        }

        private static bool IsTap(MIDNote n, List<NoteSection> tap) {
            bool check = false;
            foreach (NoteSection t in tap) {
                if (t.tick > n.tick) break;
                if (n.tick >= t.tick && n.tick < (t.tick + t.sus)) {
                    check = true; break;
                }
            }
            return check;
        }

        private static long GetNextNoteDiff(MIDNote n, List<Event> notes) {
            if (notes.IndexOf(n) == notes.Count - 1) return 1;
            for (int i = notes.IndexOf(n) + 1; i < notes.Count; i++) {
                var nextNote = notes[i] as MIDNote;
                if (nextNote != null) return notes[i].tick - n.tick;
            }
            return 1;
        }
    }
}
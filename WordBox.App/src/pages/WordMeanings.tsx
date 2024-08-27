import { useEffect, useState } from "react";
import { Result, WordDto, WordMeaningDto } from "../model/WordBoxModel";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

function WordMeanings() {
  const { wordId } = useParams();
  const [word, setWord] = useState<WordDto>({} as WordDto);
  const [wordMeanings, setWordMeanings] = useState<WordMeaningDto[]>([]);
  const [newWordMeaning, setNewWordMeaning] = useState<string>("");

  useEffect(() => {
    async function fetchWord() {
      const response = await axios.get<Result<WordDto>>(
        `https://localhost:7275/api/words/${wordId}`
      );
      if (response.data.isSuccess) {
        setWord(response.data.data!);
      }
    }

    async function fetchWordMeanings() {
      const response = await axios.get<Result<WordMeaningDto[]>>(
        `https://localhost:7275/api/wordmeanings/${wordId}`
      );
      if (response.data.isSuccess) {
        setWordMeanings(response.data.data!);
      }
    }

    fetchWord();
    fetchWordMeanings();
  }, [wordId]);

  async function addMeaning() {
    if (newWordMeaning === "") {
      return;
    }

    const response = await axios.post<Result<WordMeaningDto>>(
      `https://localhost:7275/api/wordmeanings`,
      { wordId: wordId, text: newWordMeaning }
    );

    if (response.data.isSuccess) {
      setNewWordMeaning("");
      setWordMeanings([...wordMeanings, response.data.data!]);
    }
  }

  async function deleteMeaning(wordMeaningId: string) {
    const response = await axios.delete<Result<WordMeaningDto>>(
      `https://localhost:7275/api/wordmeanings/${wordMeaningId}`
    );
    if (response.data.isSuccess) {
      setWordMeanings(wordMeanings.filter((w) => w.id !== wordMeaningId));
    }
  }

  return (
    <div className="container">
      <h1>{word.text}</h1>
      <div>
        <input
          value={newWordMeaning}
          type="text"
          placeholder="New Meaning"
          onChange={(e) => setNewWordMeaning(e.target.value)}
        />
        <button onClick={addMeaning}>
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
            <path d="M440-440H200v-80h240v-240h80v240h240v80H520v240h-80v-240Z" />
          </svg>
        </button>
      </div>
      <ul className="list">
        {wordMeanings.map((w) => (
          <li key={w.id}>
            <Link to={`/meanings/${w.id}`}>{w.text}</Link>
            <button onClick={() => deleteMeaning(w.id)}>
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
                <path d="M280-120q-33 0-56.5-23.5T200-200v-520h-40v-80h200v-40h240v40h200v80h-40v520q0 33-23.5 56.5T680-120H280Zm400-600H280v520h400v-520ZM360-280h80v-360h-80v360Zm160 0h80v-360h-80v360ZM280-720v520-520Z" />
              </svg>
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default WordMeanings;

import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { LanguageDto, Result, WordDto } from "../model/WordBoxModel";

function LanguageWords() {
  const [language, setLanguage] = useState<LanguageDto>({} as LanguageDto);
  const { languageId } = useParams();
  const [words, setWords] = useState<WordDto[]>([]);
  const [newWord, setNewWord] = useState<string>("");

  useEffect(() => {
    async function fetchWords() {
      const response = await axios.get<Result<WordDto[]>>(
        `https://localhost:7275/api/words/languagewords/${languageId}`
      );
      if (response.data.isSuccess) {
        setWords(response.data.data!);
      }
    }

    async function fetchLanguageName() {
      const response = await axios.get<Result<LanguageDto>>(
        `https://localhost:7275/api/languages/${languageId}`
      );
      if (response !== null && response.data.isSuccess) {
        setLanguage(response.data.data!);
      }
    }

    fetchLanguageName();
    fetchWords();
  }, [languageId]);

  async function addWord() {
    if (newWord === "") {
      return;
    }

    const response = await axios.post<Result<WordDto>>(
      `https://localhost:7275/api/words`,
      { languageId: languageId, text: newWord }
    );
    if (response.data.isSuccess) {
      setNewWord("");
      setWords([...words, response.data.data!]);
    }
  }

  async function deleteWord(wordId: string) {
    const response = await axios.delete<Result<WordDto>>(
      `https://localhost:7275/api/words/${wordId}`
    );
    if (response.data.isSuccess) {
      setWords(words.filter((w) => w.id !== wordId));
    }
  }

  return (
    <div className="container">
      <h1>{language.name}</h1>
      <div>
        <input
          type="text"
          placeholder="Add Word"
          value={newWord}
          onChange={(e) => setNewWord(e.target.value)}
        />
        <button onClick={addWord}>
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
            <path d="M440-440H200v-80h240v-240h80v240h240v80H520v240h-80v-240Z" />
          </svg>
        </button>
      </div>
      <ul className="list">
        {words.map((w) => (
          <li key={w.id}>
            <Link to={`/meanings/${w.id}`}>{w.text}</Link>
            <button onClick={() => deleteWord(w.id)}>
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

export default LanguageWords;

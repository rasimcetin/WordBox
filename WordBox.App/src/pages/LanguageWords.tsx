import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { LanguageDto, Result, WordDto } from "../model/WordBoxModel";

function LanguageWords() {
  const [language, setLanguage] = useState<LanguageDto>({} as LanguageDto);
  const { languageId } = useParams();
  const [words, setWords] = useState<WordDto[]>([]);

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

  return (
    <div className="container">
      <h1>{language.name}</h1>
      <ul className="list">
        {words.map((w) => (
          <li key={w.id}>
            <div>{w.text}</div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default LanguageWords;

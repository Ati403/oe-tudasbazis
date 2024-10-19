from transformers import AutoModel, AutoTokenizer
import torch

model_name = "danieleff/hubert-base-cc-sentence-transformer"
output_path = ".\sentence_transformer.onnx"
model = AutoModel.from_pretrained(model_name)
tokenizer = AutoTokenizer.from_pretrained(model_name)

dummy_input = tokenizer("Ez egy teszt mondat.", return_tensors="pt", padding=True, truncation=True)
torch.onnx.export(model,
                  (dummy_input["input_ids"], dummy_input["attention_mask"]),
                  output_path,
                  input_names=["input_ids", "attention_mask"],
                  output_names=["last_hidden_state"],
                  opset_version=14)
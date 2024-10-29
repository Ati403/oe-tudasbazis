import torch
from transformers import AutoModel

model_name = "danieleff/hubert-base-cc-sentence-transformer"
model = AutoModel.from_pretrained(model_name)
model.eval()

inputs = {
        # list of numerical ids for the tokenized text
        'input_ids':   torch.randint(32, [1, 512], dtype=torch.long), 
        # dummy list of ones
        'attention_mask': torch.ones([1, 512], dtype=torch.long),  
    }

symbolic_names = {0: 'batch_size', 1: 'max_seq_len'}

torch.onnx.export(model,
                  (inputs['input_ids'], inputs['attention_mask']),
                  "sentence_transformer.onnx",
                  opset_version=14,
                  do_constant_folding=True,
                  input_names=['input_ids', 'attention_mask'],
                  output_names=['last_hidden_state'],
                  dynamic_axes={'input_ids': symbolic_names, 'attention_mask': symbolic_names, 'last_hidden_state': symbolic_names})
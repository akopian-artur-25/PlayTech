export function insertString(string, index, substring) {
  return [string.slice(0, index), substring, string.slice(index)].join('');
}

export function cleanHtml(value) {
  return value ? value.replace(/<[^>]*>?/gm, '') : '';
}

import { Text } from '@mantine/core';

export default function Goals() {
    const tableRows = elements.map((goalData) => (
    <Table.Tr key={goalData.name}>
      <Table.Td>{goalData.name}</Table.Td>
      <Table.Td>{goalData.priority}</Table.Td>
      <Table.Td>{goalData.type}</Table.Td>
      <Table.Td>{goalData.streak}</Table.Td>
    </Table.Tr>
  ));

    return (
    <Table>
      <Table.Thead>
        <Table.Tr>
          <Table.Th>Name</Table.Th>
          <Table.Th>Priority</Table.Th>
          <Table.Th>Type</Table.Th>
          <Table.Th>Streak</Table.Th>
        </Table.Tr>
      </Table.Thead>
      <Table.Tbody>{tableRows}</Table.Tbody>
    </Table>
  );
}
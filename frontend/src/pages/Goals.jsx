import { Button, Modal, Table, Fieldset, TextInput, Textarea, NativeSelect, SimpleGrid } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { useForm } from '@mantine/form';

export default function Goals() {
  const [opened, { open, close }] = useDisclosure(false);
  const form = useForm({
    mode: 'uncontrolled',
    initialValues: {
      email: '',
      termsOfService: false,
    },

    validate: {
      email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email'),
    },
  });

  const elements = [];

  const tableRows = elements.map((goalData) => (
    <Table.Tr key={goalData.name}>
      <Table.Td>{goalData.name}</Table.Td>
      <Table.Td>{goalData.priority}</Table.Td>
      <Table.Td>{goalData.type}</Table.Td>
      <Table.Td>{goalData.streak}</Table.Td>
    </Table.Tr>
  ));

  return (
    <>
      <Button
        variant="filled"
        onClick={open}
      >Create</Button>
      <Modal
        opened={opened}
        onClose={close}
        title="Create a new goal"
        fullScreen
        radius={0}
        transitionProps={{ transition: 'fade', duration: 200 }}
      >
        <Fieldset legend="Definition">
          <TextInput
            label="Name"
          />
          <TextInput
            label="Description"
          />
          <NativeSelect
            label="Type"
            data={["Superordinate", "Intermediate", "Subordinate"]}
          />
        </Fieldset>
        <Fieldset legend="Reflection">
          <Textarea
            label="Justification"
            description="Why do you want to achieve this goal? Make a connection to your identity and your values."
          />
          <Textarea
            label="Importance"
            description="How important is this goal to you?"
          />
          <Textarea
            label="Difficulty"
            description="How difficult do you think achieving this goal will be? Are you willing to take on the challenge and endure the pain?"
          />
          <SimpleGrid cols={2}>
            <Textarea
              label="Strengths"
              description="What relevant strengths do you have?"
            />
            <Textarea
              label="Weaknesses"
              description="What relevant weaknesses do you have?"
            />
          </SimpleGrid>
          <Textarea
            label="Obstacles"
            description="What obstacles might you encounter while progressing towards your goal? How will you overcome them?"
          />
          <Textarea
            label="Motivation"
            description="How will you maintain motivation while working towards your goal? Will you use the carrot or the stick? Make sure to include 'slack with a cost' options."
          />
          <Textarea
            label="Kill conditions"
            description="When will you consider giving up on your goal to avoid sunk cost fallacy?"
          />
        </Fieldset>
        <Fieldset legend="Systems">
          <Textarea
            label="Structure"
            description="How will you implement steps to achieve your goal?"
          />
          <Textarea
            label="Trigger management"
            description="What steps will you take to make your goal the default decision?"
          />
          <Textarea
            label="Temptation manegement"
            description="How will you remove temptations and make non-goal decisions harder?"
          />
          <Textarea
            label="Commitment"
            description="Which strategies will you employ to opt in ahead of time and making opting out harder?"
          />
          <Table>

          </Table>
        </Fieldset>
      </Modal>
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
    </>
  );
}